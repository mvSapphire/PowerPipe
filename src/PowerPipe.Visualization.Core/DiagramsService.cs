using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Antlr4.Runtime;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.TypeSystem;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PowerPipe.Interfaces;
using PowerPipe.Visualization.Core.Antlr;
using PowerPipe.Visualization.Core.Configurations;
using PowerPipe.Visualization.Core.Mermaid.Graph.Interfaces;

namespace PowerPipe.Visualization.Core;

public interface IDiagramService
{
    ICollection<string> GetDiagrams();
}

public class DiagramsService : IDiagramService
{
    private static readonly Regex PipelineBuilderRegex = new Regex("(new PipelineBuilder)[\\s\\S]*(;)", RegexOptions.Compiled);

    private readonly PowerPipeVisualizationConfiguration _configuration;

    private readonly ILogger<DiagramsService> _logger;

    private readonly List<string> _diagrams;

    public DiagramsService(
        IOptions<PowerPipeVisualizationConfiguration> configuration,
        ILoggerFactory loggerFactory)
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
        {
            return _diagrams;
        }

        try
        {
            foreach (var assembly in _configuration.AssembliesToScan.Where(it => it is not null))
            {
                foreach (var decompiledType in GetPipelineBuilders(assembly))
                {
                    if (string.IsNullOrEmpty(decompiledType))
                    {
                        continue;
                    }

                    _diagrams.Add(ProcessDecompiledType(decompiledType));
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogDebug("Exception occured during retrieving of diagrams. {Exception}", e);
        }

        return _diagrams;
    }

    private IEnumerable<string> GetPipelineBuilders(Assembly assembly)
    {
        var typesToDecompile = GetTypesToDecompile(assembly);

        if (typesToDecompile.Count == 0)
        {
            return Enumerable.Empty<string>();
        }

        var decompiler = new CSharpDecompiler(assembly.Location, new DecompilerSettings());

        return typesToDecompile.Select(type =>
        {
            var decompiled = decompiler.DecompileTypeAsString(new FullTypeName(type));

            return PipelineBuilderRegex.Match(decompiled).ToString();
        });
    }

    private IReadOnlyCollection<string> GetTypesToDecompile(Assembly assembly)
    {
        var result = new List<string>();

        foreach (var type in assembly.GetTypes())
        {
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            if (fields.Any(it => it.FieldType == typeof(IPipelineStepFactory)))
            {
                result.Add(type.FullName);
            }
        }

        if (result.Count == 0)
        {
            _logger.LogDebug("No files that contain PipelineBuilder");
        }

        return result;
    }

    private string ProcessDecompiledType(string input)
    {
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
