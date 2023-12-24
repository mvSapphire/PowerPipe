﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/maksym.vorchakov/Work/Documents/Projects/Personal/PowerPipe/src/PowerPipe.Visualization.Core/Antlr/PipelineLexer.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace PowerPipe.Visualization.Core.Antlr {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class PipelineLexer : Lexer {
	public const int
		NEWBUIDLER=1, LAMBDANAME=2, LAMBDA=3, ADD=4, ADDIF=5, ADDIFELSE=6, PARALLEL=7, 
		IF=8, PREDICATE=9, DATA=10, IDENTIFIER=11, LEFTARROW=12, RIGHTARROW=13, 
		OPENPAR=14, CLOSEPAR=15, EMPTYPAR=16, COMA=17, DOT=18, WS=19, ONERRORRETRY=20, 
		ONERRORSUPPRESS=21, COMPENSATE=22;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"NEWBUIDLER", "LAMBDANAME", "LAMBDA", "ADD", "ADDIF", "ADDIFELSE", "PARALLEL", 
		"IF", "PREDICATE", "DATA", "IDENTIFIER", "LEFTARROW", "RIGHTARROW", "OPENPAR", 
		"CLOSEPAR", "EMPTYPAR", "COMA", "DOT", "WS", "ONERRORRETRY", "ONERRORSUPPRESS", 
		"COMPENSATE"
	};


	public PipelineLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, "'Add'", "'AddIf'", "'AddIfElse'", "'Parallel'", 
		"'If'", null, null, null, "'<'", "'>'", "'('", "')'", "'()'", "','", "'.'", 
		null, "'.OnError(PipelineStepErrorHandling.Retry)'", "'.OnError(PipelineStepErrorHandling.Suppress)'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "NEWBUIDLER", "LAMBDANAME", "LAMBDA", "ADD", "ADDIF", "ADDIFELSE", 
		"PARALLEL", "IF", "PREDICATE", "DATA", "IDENTIFIER", "LEFTARROW", "RIGHTARROW", 
		"OPENPAR", "CLOSEPAR", "EMPTYPAR", "COMA", "DOT", "WS", "ONERRORRETRY", 
		"ONERRORSUPPRESS", "COMPENSATE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "PipelineLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x18\x140\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3"+
		"\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2"+
		"\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3"+
		"\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x6\x3V\n\x3\r\x3\xE"+
		"\x3W\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3"+
		"\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3"+
		"\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3"+
		"\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\v\x6"+
		"\v\xA3\n\v\r\v\xE\v\xA4\x3\f\x3\f\a\f\xA9\n\f\f\f\xE\f\xAC\v\f\x3\r\x3"+
		"\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\x10\x3\x10\x3\x11"+
		"\x3\x11\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13"+
		"\x3\x13\x3\x13\x3\x14\x6\x14\xC8\n\x14\r\x14\xE\x14\xC9\x3\x14\x3\x14"+
		"\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15"+
		"\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15"+
		"\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15"+
		"\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15\x3\x15"+
		"\x3\x15\x3\x15\x3\x15\x3\x15\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16\x3\x16"+
		"\x3\x16\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17"+
		"\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17"+
		"\x3\x17\x3\x17\x3\x17\x3\x17\x3\x17\x2\x2\x2\x18\x3\x2\x3\x5\x2\x4\a\x2"+
		"\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r"+
		"\x19\x2\xE\x1B\x2\xF\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2"+
		"\x15)\x2\x16+\x2\x17-\x2\x18\x3\x2\x6\x3\x2\x63|\x6\x2\x32;\x43\\\x61"+
		"\x61\x63|\x4\x2\x43\\\x63|\x5\x2\v\f\xF\xF\"\"\x143\x2\x3\x3\x2\x2\x2"+
		"\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2"+
		"\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2"+
		"\x2\x2\x15\x3\x2\x2\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3"+
		"\x2\x2\x2\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3"+
		"\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2"+
		"\x2\x2-\x3\x2\x2\x2\x3/\x3\x2\x2\x2\x5U\x3\x2\x2\x2\a[\x3\x2\x2\x2\t}"+
		"\x3\x2\x2\x2\v\x81\x3\x2\x2\x2\r\x87\x3\x2\x2\x2\xF\x91\x3\x2\x2\x2\x11"+
		"\x9A\x3\x2\x2\x2\x13\x9D\x3\x2\x2\x2\x15\xA2\x3\x2\x2\x2\x17\xA6\x3\x2"+
		"\x2\x2\x19\xAD\x3\x2\x2\x2\x1B\xB1\x3\x2\x2\x2\x1D\xB5\x3\x2\x2\x2\x1F"+
		"\xB7\x3\x2\x2\x2!\xB9\x3\x2\x2\x2#\xBE\x3\x2\x2\x2%\xC2\x3\x2\x2\x2\'"+
		"\xC7\x3\x2\x2\x2)\xCD\x3\x2\x2\x2+\xF9\x3\x2\x2\x2-\x128\x3\x2\x2\x2/"+
		"\x30\ap\x2\x2\x30\x31\ag\x2\x2\x31\x32\ay\x2\x2\x32\x33\a\"\x2\x2\x33"+
		"\x34\aR\x2\x2\x34\x35\ak\x2\x2\x35\x36\ar\x2\x2\x36\x37\ag\x2\x2\x37\x38"+
		"\an\x2\x2\x38\x39\ak\x2\x2\x39:\ap\x2\x2:;\ag\x2\x2;<\a\x44\x2\x2<=\a"+
		"w\x2\x2=>\ak\x2\x2>?\an\x2\x2?@\a\x66\x2\x2@\x41\ag\x2\x2\x41\x42\at\x2"+
		"\x2\x42\x43\a>\x2\x2\x43\x44\x3\x2\x2\x2\x44\x45\x5\x15\v\x2\x45\x46\a"+
		".\x2\x2\x46G\a\"\x2\x2GH\x3\x2\x2\x2HI\x5\x15\v\x2IJ\a@\x2\x2JK\a*\x2"+
		"\x2KL\x5\x15\v\x2LM\a.\x2\x2MN\a\"\x2\x2NO\x3\x2\x2\x2OP\x5\x15\v\x2P"+
		"Q\a+\x2\x2QR\x3\x2\x2\x2RS\b\x2\x2\x2S\x4\x3\x2\x2\x2TV\t\x2\x2\x2UT\x3"+
		"\x2\x2\x2VW\x3\x2\x2\x2WU\x3\x2\x2\x2WX\x3\x2\x2\x2XY\x3\x2\x2\x2YZ\b"+
		"\x3\x2\x2Z\x6\x3\x2\x2\x2[\\\a*\x2\x2\\]\aR\x2\x2]^\ak\x2\x2^_\ar\x2\x2"+
		"_`\ag\x2\x2`\x61\an\x2\x2\x61\x62\ak\x2\x2\x62\x63\ap\x2\x2\x63\x64\a"+
		"g\x2\x2\x64\x65\a\x44\x2\x2\x65\x66\aw\x2\x2\x66g\ak\x2\x2gh\an\x2\x2"+
		"hi\a\x66\x2\x2ij\ag\x2\x2jk\at\x2\x2kl\a>\x2\x2lm\x3\x2\x2\x2mn\x5\x15"+
		"\v\x2no\a.\x2\x2op\a\"\x2\x2pq\x3\x2\x2\x2qr\x5\x15\v\x2rs\a@\x2\x2st"+
		"\a\"\x2\x2tu\x3\x2\x2\x2uv\x5\x15\v\x2vw\a+\x2\x2wx\a\"\x2\x2xy\a?\x2"+
		"\x2yz\a@\x2\x2z{\x3\x2\x2\x2{|\b\x4\x2\x2|\b\x3\x2\x2\x2}~\a\x43\x2\x2"+
		"~\x7F\a\x66\x2\x2\x7F\x80\a\x66\x2\x2\x80\n\x3\x2\x2\x2\x81\x82\a\x43"+
		"\x2\x2\x82\x83\a\x66\x2\x2\x83\x84\a\x66\x2\x2\x84\x85\aK\x2\x2\x85\x86"+
		"\ah\x2\x2\x86\f\x3\x2\x2\x2\x87\x88\a\x43\x2\x2\x88\x89\a\x66\x2\x2\x89"+
		"\x8A\a\x66\x2\x2\x8A\x8B\aK\x2\x2\x8B\x8C\ah\x2\x2\x8C\x8D\aG\x2\x2\x8D"+
		"\x8E\an\x2\x2\x8E\x8F\au\x2\x2\x8F\x90\ag\x2\x2\x90\xE\x3\x2\x2\x2\x91"+
		"\x92\aR\x2\x2\x92\x93\a\x63\x2\x2\x93\x94\at\x2\x2\x94\x95\a\x63\x2\x2"+
		"\x95\x96\an\x2\x2\x96\x97\an\x2\x2\x97\x98\ag\x2\x2\x98\x99\an\x2\x2\x99"+
		"\x10\x3\x2\x2\x2\x9A\x9B\aK\x2\x2\x9B\x9C\ah\x2\x2\x9C\x12\x3\x2\x2\x2"+
		"\x9D\x9E\a*\x2\x2\x9E\x9F\x5\x17\f\x2\x9F\xA0\a+\x2\x2\xA0\x14\x3\x2\x2"+
		"\x2\xA1\xA3\t\x3\x2\x2\xA2\xA1\x3\x2\x2\x2\xA3\xA4\x3\x2\x2\x2\xA4\xA2"+
		"\x3\x2\x2\x2\xA4\xA5\x3\x2\x2\x2\xA5\x16\x3\x2\x2\x2\xA6\xAA\t\x4\x2\x2"+
		"\xA7\xA9\t\x3\x2\x2\xA8\xA7\x3\x2\x2\x2\xA9\xAC\x3\x2\x2\x2\xAA\xA8\x3"+
		"\x2\x2\x2\xAA\xAB\x3\x2\x2\x2\xAB\x18\x3\x2\x2\x2\xAC\xAA\x3\x2\x2\x2"+
		"\xAD\xAE\a>\x2\x2\xAE\xAF\x3\x2\x2\x2\xAF\xB0\b\r\x2\x2\xB0\x1A\x3\x2"+
		"\x2\x2\xB1\xB2\a@\x2\x2\xB2\xB3\x3\x2\x2\x2\xB3\xB4\b\xE\x2\x2\xB4\x1C"+
		"\x3\x2\x2\x2\xB5\xB6\a*\x2\x2\xB6\x1E\x3\x2\x2\x2\xB7\xB8\a+\x2\x2\xB8"+
		" \x3\x2\x2\x2\xB9\xBA\a*\x2\x2\xBA\xBB\a+\x2\x2\xBB\xBC\x3\x2\x2\x2\xBC"+
		"\xBD\b\x11\x2\x2\xBD\"\x3\x2\x2\x2\xBE\xBF\a.\x2\x2\xBF\xC0\x3\x2\x2\x2"+
		"\xC0\xC1\b\x12\x2\x2\xC1$\x3\x2\x2\x2\xC2\xC3\a\x30\x2\x2\xC3\xC4\x3\x2"+
		"\x2\x2\xC4\xC5\b\x13\x2\x2\xC5&\x3\x2\x2\x2\xC6\xC8\t\x5\x2\x2\xC7\xC6"+
		"\x3\x2\x2\x2\xC8\xC9\x3\x2\x2\x2\xC9\xC7\x3\x2\x2\x2\xC9\xCA\x3\x2\x2"+
		"\x2\xCA\xCB\x3\x2\x2\x2\xCB\xCC\b\x14\x2\x2\xCC(\x3\x2\x2\x2\xCD\xCE\a"+
		"\x30\x2\x2\xCE\xCF\aQ\x2\x2\xCF\xD0\ap\x2\x2\xD0\xD1\aG\x2\x2\xD1\xD2"+
		"\at\x2\x2\xD2\xD3\at\x2\x2\xD3\xD4\aq\x2\x2\xD4\xD5\at\x2\x2\xD5\xD6\a"+
		"*\x2\x2\xD6\xD7\aR\x2\x2\xD7\xD8\ak\x2\x2\xD8\xD9\ar\x2\x2\xD9\xDA\ag"+
		"\x2\x2\xDA\xDB\an\x2\x2\xDB\xDC\ak\x2\x2\xDC\xDD\ap\x2\x2\xDD\xDE\ag\x2"+
		"\x2\xDE\xDF\aU\x2\x2\xDF\xE0\av\x2\x2\xE0\xE1\ag\x2\x2\xE1\xE2\ar\x2\x2"+
		"\xE2\xE3\aG\x2\x2\xE3\xE4\at\x2\x2\xE4\xE5\at\x2\x2\xE5\xE6\aq\x2\x2\xE6"+
		"\xE7\at\x2\x2\xE7\xE8\aJ\x2\x2\xE8\xE9\a\x63\x2\x2\xE9\xEA\ap\x2\x2\xEA"+
		"\xEB\a\x66\x2\x2\xEB\xEC\an\x2\x2\xEC\xED\ak\x2\x2\xED\xEE\ap\x2\x2\xEE"+
		"\xEF\ai\x2\x2\xEF\xF0\a\x30\x2\x2\xF0\xF1\aT\x2\x2\xF1\xF2\ag\x2\x2\xF2"+
		"\xF3\av\x2\x2\xF3\xF4\at\x2\x2\xF4\xF5\a{\x2\x2\xF5\xF6\a+\x2\x2\xF6\xF7"+
		"\x3\x2\x2\x2\xF7\xF8\b\x15\x2\x2\xF8*\x3\x2\x2\x2\xF9\xFA\a\x30\x2\x2"+
		"\xFA\xFB\aQ\x2\x2\xFB\xFC\ap\x2\x2\xFC\xFD\aG\x2\x2\xFD\xFE\at\x2\x2\xFE"+
		"\xFF\at\x2\x2\xFF\x100\aq\x2\x2\x100\x101\at\x2\x2\x101\x102\a*\x2\x2"+
		"\x102\x103\aR\x2\x2\x103\x104\ak\x2\x2\x104\x105\ar\x2\x2\x105\x106\a"+
		"g\x2\x2\x106\x107\an\x2\x2\x107\x108\ak\x2\x2\x108\x109\ap\x2\x2\x109"+
		"\x10A\ag\x2\x2\x10A\x10B\aU\x2\x2\x10B\x10C\av\x2\x2\x10C\x10D\ag\x2\x2"+
		"\x10D\x10E\ar\x2\x2\x10E\x10F\aG\x2\x2\x10F\x110\at\x2\x2\x110\x111\a"+
		"t\x2\x2\x111\x112\aq\x2\x2\x112\x113\at\x2\x2\x113\x114\aJ\x2\x2\x114"+
		"\x115\a\x63\x2\x2\x115\x116\ap\x2\x2\x116\x117\a\x66\x2\x2\x117\x118\a"+
		"n\x2\x2\x118\x119\ak\x2\x2\x119\x11A\ap\x2\x2\x11A\x11B\ai\x2\x2\x11B"+
		"\x11C\a\x30\x2\x2\x11C\x11D\aU\x2\x2\x11D\x11E\aw\x2\x2\x11E\x11F\ar\x2"+
		"\x2\x11F\x120\ar\x2\x2\x120\x121\at\x2\x2\x121\x122\ag\x2\x2\x122\x123"+
		"\au\x2\x2\x123\x124\au\x2\x2\x124\x125\a+\x2\x2\x125\x126\x3\x2\x2\x2"+
		"\x126\x127\b\x16\x2\x2\x127,\x3\x2\x2\x2\x128\x129\a\x30\x2\x2\x129\x12A"+
		"\a\x45\x2\x2\x12A\x12B\aq\x2\x2\x12B\x12C\ao\x2\x2\x12C\x12D\ar\x2\x2"+
		"\x12D\x12E\ag\x2\x2\x12E\x12F\ap\x2\x2\x12F\x130\au\x2\x2\x130\x131\a"+
		"\x63\x2\x2\x131\x132\av\x2\x2\x132\x133\ag\x2\x2\x133\x134\aY\x2\x2\x134"+
		"\x135\ak\x2\x2\x135\x136\av\x2\x2\x136\x137\aj\x2\x2\x137\x138\a>\x2\x2"+
		"\x138\x139\x3\x2\x2\x2\x139\x13A\x5\x15\v\x2\x13A\x13B\a@\x2\x2\x13B\x13C"+
		"\a*\x2\x2\x13C\x13D\a+\x2\x2\x13D\x13E\x3\x2\x2\x2\x13E\x13F\b\x17\x2"+
		"\x2\x13F.\x3\x2\x2\x2\a\x2W\xA4\xAA\xC9\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace PowerPipe.Visualization.Core.Antlr