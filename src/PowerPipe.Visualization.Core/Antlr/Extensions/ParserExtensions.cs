using System;
using Antlr4.Runtime.Tree;

namespace PowerPipe.Visualization.Core.Antlr.Extensions;

internal static class ParserExtensions
{
    internal static string GetStepName(this ITerminalNode node) =>
        node.GetText().TrimStart('<').TrimEnd('>').Split('<')[0];

    internal static (string, string) GetTwoStepsNames(this ITerminalNode node)
    {
        var steps = node.GetText().Split(',', StringSplitOptions.RemoveEmptyEntries);

        var step1 = steps[0].TrimStart('<').TrimEnd('>').Split('<')[0];
        var step2 = steps[1].TrimStart('<').TrimEnd('>').Split('<')[0];

        return (step1, step2);
    }

    internal static string GetPredicateName(this ITerminalNode node) =>
        node.GetText().TrimStart('(').TrimEnd(')');

    internal static string GetOpenPredicateName(this ITerminalNode node) =>
        node.GetText().TrimStart('(').TrimEnd(',');
}
