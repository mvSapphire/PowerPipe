using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Antlr4.Runtime;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.TypeSystem;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PowerPipe.Visualization.Core.Antlr;
using PowerPipe.Visualization.Core.Configurations;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Core;

public class DiagramsService : IDiagramService
{
    private static readonly Regex PipelineBuilderRegex = new Regex(@"(new PipelineBuilder)[\s\S]*(;)", RegexOptions.Compiled);

    private readonly PowerPipeVisualizationConfiguration _configuration;

    private readonly ILogger<DiagramsService> _logger;

    private readonly List<string> _diagrams;

    public DiagramsService(IOptions<PowerPipeVisualizationConfiguration> configuration, ILoggerFactory loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(loggerFactory);
        
        _configuration = configuration.Value;

        _logger = loggerFactory.CreateLogger<DiagramsService>();

        _diagrams = new List<string>();
    }

    public ICollection<string> GetDiagrams()
    {
        if (_diagrams.Count > 0)
            return _diagrams;

        try
        {
            var typesToDecompile = GetTypesToDecompile();

            var decompiledTypes = DecompileTypes(typesToDecompile);

            _diagrams.AddRange(ProcessDecompiledTypes(decompiledTypes));
        }
        catch (Exception e)
        {
            _logger.LogDebug("Exception occured during retrieving of diagrams. {Exception}", e);
        }

        return _diagrams;
    }

    private IEnumerable<Type> GetTypesToDecompile()
    {
        var types = _configuration.TypesToScan.Where(it => it is not null).ToList();

        foreach (var assembly in _configuration.AssembliesToScan.Where(it => it is not null))
            types.AddRange(assembly.GetTypes());

        return types;
    }

    private IEnumerable<string> DecompileTypes(IEnumerable<Type> types) =>
        types.Select(type =>
        {
            var decompiler = new CSharpDecompiler(type.Assembly.Location, new DecompilerSettings());

            return decompiler.DecompileTypeAsString(new FullTypeName(type.FullName));
        });

    private IEnumerable<string> ProcessDecompiledTypes(IEnumerable<string> decompiledTypes) =>
        decompiledTypes
            .Select(decompiledType =>
            {
                var input = PipelineBuilderRegex.Match(decompiledType).ToString();

                if (string.IsNullOrEmpty(input))
                {
                    return null;
                }

                var inputStream = new AntlrInputStream(input);
                var pipelineLexer = new PipelineLexer(inputStream);
                var commonTokenStream = new CommonTokenStream(pipelineLexer);
                var pipelineParser = new PipelineParser(commonTokenStream);

                var startContext = pipelineParser.start();

                var visitor = new PipelineParserVisitor();
                var graph = (IGraph)visitor.Visit(startContext);

                return graph.Render();
            })
            .Where(it => it is not null);
}
