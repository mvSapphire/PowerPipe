using Antlr4.Runtime.Tree;

namespace PowerPipe.Visualization.Core.Antlr.Extensions;

internal static class ParserExtensions
{
    internal static string GetStepName(this ITerminalNode node) =>
        node.GetText().TrimStart('<').TrimEnd('>').Split('<')[0];

    internal static string GetPredicateName(this ITerminalNode node) =>
        node.GetText().TrimStart('(').TrimEnd(')');
}
