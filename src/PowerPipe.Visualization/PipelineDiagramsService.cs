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
using PowerPipe.Visualization.Antlr;
using PowerPipe.Visualization.Configurations;
using PowerPipe.Visualization.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization;

/// <inheritdoc />
public class PipelineDiagramsService : IPipelineDiagramService
{
    private readonly Regex _pipelineBuilderRegex = new(@"(new PipelineBuilder)[\s\S]*(;)", RegexOptions.Compiled);
    private readonly Dictionary<string, string> _diagrams;

    private readonly PowerPipeVisualizationConfiguration _configuration;
    private readonly ILogger<PipelineDiagramsService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineDiagramsService"/> class.
    /// </summary>
    /// <param name="configuration">Diagrams visualization configuration</param>
    /// <param name="loggerFactory">Logger factory</param>
    public PipelineDiagramsService(IOptions<PowerPipeVisualizationConfiguration> configuration, ILoggerFactory loggerFactory)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));
        
        _configuration = configuration.Value;

        _logger = loggerFactory.CreateLogger<PipelineDiagramsService>();

        _diagrams = new Dictionary<string, string>();
    }

    /// <inheritdoc />
    public IDictionary<string, string> GetDiagrams()
    {
        if (_diagrams.Count > 0)
            return _diagrams;

        try
        {
            foreach (var type in GetTypesToDecompile())
            {
                var decompiler = new CSharpDecompiler(type.Assembly.Location, new DecompilerSettings());

                _diagrams.Add(type.Name, ProcessDecompiledType(decompiler.DecompileTypeAsString(new FullTypeName(type.FullName))));
            }
        }
        catch (Exception e)
        {
            _logger.LogDebug("Exception occured during retrieving of diagrams. {Exception}", e);
        }

        return _diagrams;
    }

    private List<Type> GetTypesToDecompile()
    {
        var types = _configuration.TypesToScan.Where(it => it is not null).ToList();

        foreach (var assembly in _configuration.AssembliesToScan.Where(it => it is not null))
            types.AddRange(assembly.GetTypes());

        return types;
    }

    private string ProcessDecompiledType(string decompiledType)
    {
        var input = _pipelineBuilderRegex.Match(decompiledType).ToString();

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
    }
}
