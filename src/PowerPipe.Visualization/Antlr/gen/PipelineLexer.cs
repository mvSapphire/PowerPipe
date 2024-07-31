﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/maksym.vorchakov/Work/Documents/Projects/Personal/PowerPipe/src/PowerPipe.Visualization/Antlr/PipelineLexer.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419
#pragma warning disable CS3021

namespace PowerPipe.Visualization.Antlr {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class PipelineLexer : Lexer {
	public const int
		ONERRORRETRY=1, ONERRORSUPPRESS=2, COMPENSATE=3, LEFTARROW=4, RIGHTARROW=5, 
		EMPTYPAR=6, COMA=7, DOT=8, WS=9, NEWBUIDLER=10, LAMBDANAME=11, LAMBDA=12, 
		ADD=13, ADDIF=14, ADDIFELSE=15, PARALLEL=16, IF=17, PREDICATE=18, OPENPREDICATE=19, 
		DATA=20, DATA2=21, STEPWITHGENERIC=22, STEPWITHOUTGENERIC=23, TWOSTEPSWITHGENERIC=24, 
		TWOSTEPSWITHOUTGENERIC=25, ANYTEXT=26, OPENPAR=27, CLOSEPAR=28;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"ONERRORRETRY", "ONERRORSUPPRESS", "COMPENSATE", "LEFTARROW", "RIGHTARROW", 
		"EMPTYPAR", "COMA", "DOT", "WS", "NEWBUIDLER", "LAMBDANAME", "LAMBDA", 
		"ADD", "ADDIF", "ADDIFELSE", "PARALLEL", "IF", "PREDICATE", "OPENPREDICATE", 
		"DATA", "DATA2", "STEPWITHGENERIC", "STEPWITHOUTGENERIC", "TWOSTEPSWITHGENERIC", 
		"TWOSTEPSWITHOUTGENERIC", "ANYTEXT", "OPENPAR", "CLOSEPAR"
	};


	public PipelineLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, "'.OnError(PipelineStepErrorHandling.Retry)'", "'.OnError(PipelineStepErrorHandling.Suppress)'", 
		null, "'<'", "'>'", "'()'", "','", "'.'", null, null, null, null, "'Add'", 
		"'AddIf'", "'AddIfElse'", "'Parallel'", "'If'", null, null, null, null, 
		null, null, null, null, null, "'('", "')'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "ONERRORRETRY", "ONERRORSUPPRESS", "COMPENSATE", "LEFTARROW", "RIGHTARROW", 
		"EMPTYPAR", "COMA", "DOT", "WS", "NEWBUIDLER", "LAMBDANAME", "LAMBDA", 
		"ADD", "ADDIF", "ADDIFELSE", "PARALLEL", "IF", "PREDICATE", "OPENPREDICATE", 
		"DATA", "DATA2", "STEPWITHGENERIC", "STEPWITHOUTGENERIC", "TWOSTEPSWITHGENERIC", 
		"TWOSTEPSWITHOUTGENERIC", "ANYTEXT", "OPENPAR", "CLOSEPAR"
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
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\x1E\x225\b\x1\x4"+
		"\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b"+
		"\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE\t\xE\x4\xF\t\xF\x4"+
		"\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15"+
		"\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A"+
		"\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2"+
		"\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3"+
		"\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2"+
		"\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3"+
		"\x2\x3\x2\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3"+
		"\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5\x3\x6\x3\x6\x3\x6"+
		"\x3\x6\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3"+
		"\t\x3\n\x6\n\xC3\n\n\r\n\xE\n\xC4\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x3\v\x3"+
		"\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3\v\x3"+
		"\v\x3\v\x3\v\x6\v\xDF\n\v\r\v\xE\v\xE0\x3\v\x3\v\x6\v\xE5\n\v\r\v\xE\v"+
		"\xE6\x3\v\x3\v\a\v\xEB\n\v\f\v\xE\v\xEE\v\v\x5\v\xF0\n\v\x3\v\x3\v\x3"+
		"\v\x3\v\x6\v\xF6\n\v\r\v\xE\v\xF7\x3\v\x3\v\x6\v\xFC\n\v\r\v\xE\v\xFD"+
		"\x3\v\x3\v\a\v\x102\n\v\f\v\xE\v\x105\v\v\x5\v\x107\n\v\x3\v\x3\v\x3\v"+
		"\x6\v\x10C\n\v\r\v\xE\v\x10D\x3\v\x3\v\x6\v\x112\n\v\r\v\xE\v\x113\x3"+
		"\v\x3\v\a\v\x118\n\v\f\v\xE\v\x11B\v\v\x5\v\x11D\n\v\x3\v\x3\v\x3\v\x3"+
		"\v\x6\v\x123\n\v\r\v\xE\v\x124\x3\v\x3\v\x6\v\x129\n\v\r\v\xE\v\x12A\x3"+
		"\v\x3\v\a\v\x12F\n\v\f\v\xE\v\x132\v\v\x5\v\x134\n\v\x3\v\x3\v\x3\v\x3"+
		"\v\x3\f\x6\f\x13B\n\f\r\f\xE\f\x13C\x3\f\x3\f\x3\r\x3\r\x3\r\x3\r\x3\r"+
		"\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r\x3\r"+
		"\x6\r\x154\n\r\r\r\xE\r\x155\x3\r\x3\r\x6\r\x15A\n\r\r\r\xE\r\x15B\x3"+
		"\r\x3\r\a\r\x160\n\r\f\r\xE\r\x163\v\r\x5\r\x165\n\r\x3\r\x3\r\x3\r\x3"+
		"\r\x6\r\x16B\n\r\r\r\xE\r\x16C\x3\r\x3\r\x6\r\x171\n\r\r\r\xE\r\x172\x3"+
		"\r\x3\r\a\r\x177\n\r\f\r\xE\r\x17A\v\r\x5\r\x17C\n\r\x3\r\x3\r\x3\r\x3"+
		"\r\x6\r\x182\n\r\r\r\xE\r\x183\x3\r\x3\r\x6\r\x188\n\r\r\r\xE\r\x189\x3"+
		"\r\x3\r\a\r\x18E\n\r\f\r\xE\r\x191\v\r\x5\r\x193\n\r\x3\r\x3\r\x3\r\x3"+
		"\r\x3\r\x3\r\x3\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF"+
		"\x3\xF\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10\x3\x10"+
		"\x3\x10\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11\x3\x11"+
		"\x3\x12\x3\x12\x3\x12\x3\x13\x3\x13\x3\x13\a\x13\x1BF\n\x13\f\x13\xE\x13"+
		"\x1C2\v\x13\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\a\x14\x1C9\n\x14\f\x14"+
		"\xE\x14\x1CC\v\x14\x3\x14\x3\x14\x3\x15\x3\x15\x5\x15\x1D2\n\x15\x3\x16"+
		"\x3\x16\x5\x16\x1D6\n\x16\x3\x17\x3\x17\x6\x17\x1DA\n\x17\r\x17\xE\x17"+
		"\x1DB\x3\x17\x3\x17\x6\x17\x1E0\n\x17\r\x17\xE\x17\x1E1\x3\x17\x3\x17"+
		"\x3\x17\x3\x18\x3\x18\x6\x18\x1E9\n\x18\r\x18\xE\x18\x1EA\x3\x18\x3\x18"+
		"\x3\x19\x3\x19\x6\x19\x1F1\n\x19\r\x19\xE\x19\x1F2\x3\x19\x3\x19\x6\x19"+
		"\x1F7\n\x19\r\x19\xE\x19\x1F8\x3\x19\x3\x19\x3\x19\x3\x19\x3\x19\x6\x19"+
		"\x200\n\x19\r\x19\xE\x19\x201\x3\x19\x3\x19\x6\x19\x206\n\x19\r\x19\xE"+
		"\x19\x207\x3\x19\x3\x19\x3\x19\x3\x1A\x3\x1A\x6\x1A\x20F\n\x1A\r\x1A\xE"+
		"\x1A\x210\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x6\x1A\x217\n\x1A\r\x1A\xE\x1A\x218"+
		"\x3\x1A\x3\x1A\x3\x1B\x6\x1B\x21E\n\x1B\r\x1B\xE\x1B\x21F\x3\x1C\x3\x1C"+
		"\x3\x1D\x3\x1D\x2\x2\x2\x1E\x3\x2\x3\x5\x2\x4\a\x2\x5\t\x2\x6\v\x2\a\r"+
		"\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x17\x2\r\x19\x2\xE\x1B\x2\xF"+
		"\x1D\x2\x10\x1F\x2\x11!\x2\x12#\x2\x13%\x2\x14\'\x2\x15)\x2\x16+\x2\x17"+
		"-\x2\x18/\x2\x19\x31\x2\x1A\x33\x2\x1B\x35\x2\x1C\x37\x2\x1D\x39\x2\x1E"+
		"\x3\x2\x6\x5\x2\v\f\xF\xF\"\"\x6\x2\x32;\x43\\\x61\x61\x63|\x3\x2\x63"+
		"|\x4\x2\x43\\\x63|\x250\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2"+
		"\x2\x2\x2\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2"+
		"\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x2\x17"+
		"\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2\x2\x1D\x3\x2\x2\x2"+
		"\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3\x2\x2\x2\x2%\x3\x2\x2\x2\x2"+
		"\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3"+
		"\x2\x2\x2\x2\x31\x3\x2\x2\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2"+
		"\x37\x3\x2\x2\x2\x2\x39\x3\x2\x2\x2\x3;\x3\x2\x2\x2\x5g\x3\x2\x2\x2\a"+
		"\x96\x3\x2\x2\x2\t\xAC\x3\x2\x2\x2\v\xB0\x3\x2\x2\x2\r\xB4\x3\x2\x2\x2"+
		"\xF\xB9\x3\x2\x2\x2\x11\xBD\x3\x2\x2\x2\x13\xC2\x3\x2\x2\x2\x15\xC8\x3"+
		"\x2\x2\x2\x17\x13A\x3\x2\x2\x2\x19\x140\x3\x2\x2\x2\x1B\x19B\x3\x2\x2"+
		"\x2\x1D\x19F\x3\x2\x2\x2\x1F\x1A5\x3\x2\x2\x2!\x1AF\x3\x2\x2\x2#\x1B8"+
		"\x3\x2\x2\x2%\x1BB\x3\x2\x2\x2\'\x1C5\x3\x2\x2\x2)\x1D1\x3\x2\x2\x2+\x1D5"+
		"\x3\x2\x2\x2-\x1D7\x3\x2\x2\x2/\x1E6\x3\x2\x2\x2\x31\x1EE\x3\x2\x2\x2"+
		"\x33\x20C\x3\x2\x2\x2\x35\x21D\x3\x2\x2\x2\x37\x221\x3\x2\x2\x2\x39\x223"+
		"\x3\x2\x2\x2;<\a\x30\x2\x2<=\aQ\x2\x2=>\ap\x2\x2>?\aG\x2\x2?@\at\x2\x2"+
		"@\x41\at\x2\x2\x41\x42\aq\x2\x2\x42\x43\at\x2\x2\x43\x44\a*\x2\x2\x44"+
		"\x45\aR\x2\x2\x45\x46\ak\x2\x2\x46G\ar\x2\x2GH\ag\x2\x2HI\an\x2\x2IJ\a"+
		"k\x2\x2JK\ap\x2\x2KL\ag\x2\x2LM\aU\x2\x2MN\av\x2\x2NO\ag\x2\x2OP\ar\x2"+
		"\x2PQ\aG\x2\x2QR\at\x2\x2RS\at\x2\x2ST\aq\x2\x2TU\at\x2\x2UV\aJ\x2\x2"+
		"VW\a\x63\x2\x2WX\ap\x2\x2XY\a\x66\x2\x2YZ\an\x2\x2Z[\ak\x2\x2[\\\ap\x2"+
		"\x2\\]\ai\x2\x2]^\a\x30\x2\x2^_\aT\x2\x2_`\ag\x2\x2`\x61\av\x2\x2\x61"+
		"\x62\at\x2\x2\x62\x63\a{\x2\x2\x63\x64\a+\x2\x2\x64\x65\x3\x2\x2\x2\x65"+
		"\x66\b\x2\x2\x2\x66\x4\x3\x2\x2\x2gh\a\x30\x2\x2hi\aQ\x2\x2ij\ap\x2\x2"+
		"jk\aG\x2\x2kl\at\x2\x2lm\at\x2\x2mn\aq\x2\x2no\at\x2\x2op\a*\x2\x2pq\a"+
		"R\x2\x2qr\ak\x2\x2rs\ar\x2\x2st\ag\x2\x2tu\an\x2\x2uv\ak\x2\x2vw\ap\x2"+
		"\x2wx\ag\x2\x2xy\aU\x2\x2yz\av\x2\x2z{\ag\x2\x2{|\ar\x2\x2|}\aG\x2\x2"+
		"}~\at\x2\x2~\x7F\at\x2\x2\x7F\x80\aq\x2\x2\x80\x81\at\x2\x2\x81\x82\a"+
		"J\x2\x2\x82\x83\a\x63\x2\x2\x83\x84\ap\x2\x2\x84\x85\a\x66\x2\x2\x85\x86"+
		"\an\x2\x2\x86\x87\ak\x2\x2\x87\x88\ap\x2\x2\x88\x89\ai\x2\x2\x89\x8A\a"+
		"\x30\x2\x2\x8A\x8B\aU\x2\x2\x8B\x8C\aw\x2\x2\x8C\x8D\ar\x2\x2\x8D\x8E"+
		"\ar\x2\x2\x8E\x8F\at\x2\x2\x8F\x90\ag\x2\x2\x90\x91\au\x2\x2\x91\x92\a"+
		"u\x2\x2\x92\x93\a+\x2\x2\x93\x94\x3\x2\x2\x2\x94\x95\b\x3\x2\x2\x95\x6"+
		"\x3\x2\x2\x2\x96\x97\a\x30\x2\x2\x97\x98\a\x45\x2\x2\x98\x99\aq\x2\x2"+
		"\x99\x9A\ao\x2\x2\x9A\x9B\ar\x2\x2\x9B\x9C\ag\x2\x2\x9C\x9D\ap\x2\x2\x9D"+
		"\x9E\au\x2\x2\x9E\x9F\a\x63\x2\x2\x9F\xA0\av\x2\x2\xA0\xA1\ag\x2\x2\xA1"+
		"\xA2\aY\x2\x2\xA2\xA3\ak\x2\x2\xA3\xA4\av\x2\x2\xA4\xA5\aj\x2\x2\xA5\xA6"+
		"\x3\x2\x2\x2\xA6\xA7\x5)\x15\x2\xA7\xA8\a*\x2\x2\xA8\xA9\a+\x2\x2\xA9"+
		"\xAA\x3\x2\x2\x2\xAA\xAB\b\x4\x2\x2\xAB\b\x3\x2\x2\x2\xAC\xAD\a>\x2\x2"+
		"\xAD\xAE\x3\x2\x2\x2\xAE\xAF\b\x5\x2\x2\xAF\n\x3\x2\x2\x2\xB0\xB1\a@\x2"+
		"\x2\xB1\xB2\x3\x2\x2\x2\xB2\xB3\b\x6\x2\x2\xB3\f\x3\x2\x2\x2\xB4\xB5\a"+
		"*\x2\x2\xB5\xB6\a+\x2\x2\xB6\xB7\x3\x2\x2\x2\xB7\xB8\b\a\x2\x2\xB8\xE"+
		"\x3\x2\x2\x2\xB9\xBA\a.\x2\x2\xBA\xBB\x3\x2\x2\x2\xBB\xBC\b\b\x2\x2\xBC"+
		"\x10\x3\x2\x2\x2\xBD\xBE\a\x30\x2\x2\xBE\xBF\x3\x2\x2\x2\xBF\xC0\b\t\x2"+
		"\x2\xC0\x12\x3\x2\x2\x2\xC1\xC3\t\x2\x2\x2\xC2\xC1\x3\x2\x2\x2\xC3\xC4"+
		"\x3\x2\x2\x2\xC4\xC2\x3\x2\x2\x2\xC4\xC5\x3\x2\x2\x2\xC5\xC6\x3\x2\x2"+
		"\x2\xC6\xC7\b\n\x2\x2\xC7\x14\x3\x2\x2\x2\xC8\xC9\ap\x2\x2\xC9\xCA\ag"+
		"\x2\x2\xCA\xCB\ay\x2\x2\xCB\xCC\a\"\x2\x2\xCC\xCD\aR\x2\x2\xCD\xCE\ak"+
		"\x2\x2\xCE\xCF\ar\x2\x2\xCF\xD0\ag\x2\x2\xD0\xD1\an\x2\x2\xD1\xD2\ak\x2"+
		"\x2\xD2\xD3\ap\x2\x2\xD3\xD4\ag\x2\x2\xD4\xD5\a\x44\x2\x2\xD5\xD6\aw\x2"+
		"\x2\xD6\xD7\ak\x2\x2\xD7\xD8\an\x2\x2\xD8\xD9\a\x66\x2\x2\xD9\xDA\ag\x2"+
		"\x2\xDA\xDB\at\x2\x2\xDB\xDC\a>\x2\x2\xDC\xEF\x3\x2\x2\x2\xDD\xDF\t\x3"+
		"\x2\x2\xDE\xDD\x3\x2\x2\x2\xDF\xE0\x3\x2\x2\x2\xE0\xDE\x3\x2\x2\x2\xE0"+
		"\xE1\x3\x2\x2\x2\xE1\xE2\x3\x2\x2\x2\xE2\xE4\a>\x2\x2\xE3\xE5\t\x3\x2"+
		"\x2\xE4\xE3\x3\x2\x2\x2\xE5\xE6\x3\x2\x2\x2\xE6\xE4\x3\x2\x2\x2\xE6\xE7"+
		"\x3\x2\x2\x2\xE7\xE8\x3\x2\x2\x2\xE8\xF0\a@\x2\x2\xE9\xEB\t\x3\x2\x2\xEA"+
		"\xE9\x3\x2\x2\x2\xEB\xEE\x3\x2\x2\x2\xEC\xEA\x3\x2\x2\x2\xEC\xED\x3\x2"+
		"\x2\x2\xED\xF0\x3\x2\x2\x2\xEE\xEC\x3\x2\x2\x2\xEF\xDE\x3\x2\x2\x2\xEF"+
		"\xEC\x3\x2\x2\x2\xF0\xF1\x3\x2\x2\x2\xF1\xF2\a.\x2\x2\xF2\xF3\a\"\x2\x2"+
		"\xF3\x106\x3\x2\x2\x2\xF4\xF6\t\x3\x2\x2\xF5\xF4\x3\x2\x2\x2\xF6\xF7\x3"+
		"\x2\x2\x2\xF7\xF5\x3\x2\x2\x2\xF7\xF8\x3\x2\x2\x2\xF8\xF9\x3\x2\x2\x2"+
		"\xF9\xFB\a>\x2\x2\xFA\xFC\t\x3\x2\x2\xFB\xFA\x3\x2\x2\x2\xFC\xFD\x3\x2"+
		"\x2\x2\xFD\xFB\x3\x2\x2\x2\xFD\xFE\x3\x2\x2\x2\xFE\xFF\x3\x2\x2\x2\xFF"+
		"\x107\a@\x2\x2\x100\x102\t\x3\x2\x2\x101\x100\x3\x2\x2\x2\x102\x105\x3"+
		"\x2\x2\x2\x103\x101\x3\x2\x2\x2\x103\x104\x3\x2\x2\x2\x104\x107\x3\x2"+
		"\x2\x2\x105\x103\x3\x2\x2\x2\x106\xF5\x3\x2\x2\x2\x106\x103\x3\x2\x2\x2"+
		"\x107\x108\x3\x2\x2\x2\x108\x109\a@\x2\x2\x109\x11C\a*\x2\x2\x10A\x10C"+
		"\t\x3\x2\x2\x10B\x10A\x3\x2\x2\x2\x10C\x10D\x3\x2\x2\x2\x10D\x10B\x3\x2"+
		"\x2\x2\x10D\x10E\x3\x2\x2\x2\x10E\x10F\x3\x2\x2\x2\x10F\x111\a>\x2\x2"+
		"\x110\x112\t\x3\x2\x2\x111\x110\x3\x2\x2\x2\x112\x113\x3\x2\x2\x2\x113"+
		"\x111\x3\x2\x2\x2\x113\x114\x3\x2\x2\x2\x114\x115\x3\x2\x2\x2\x115\x11D"+
		"\a@\x2\x2\x116\x118\t\x3\x2\x2\x117\x116\x3\x2\x2\x2\x118\x11B\x3\x2\x2"+
		"\x2\x119\x117\x3\x2\x2\x2\x119\x11A\x3\x2\x2\x2\x11A\x11D\x3\x2\x2\x2"+
		"\x11B\x119\x3\x2\x2\x2\x11C\x10B\x3\x2\x2\x2\x11C\x119\x3\x2\x2\x2\x11D"+
		"\x11E\x3\x2\x2\x2\x11E\x11F\a.\x2\x2\x11F\x120\a\"\x2\x2\x120\x133\x3"+
		"\x2\x2\x2\x121\x123\t\x3\x2\x2\x122\x121\x3\x2\x2\x2\x123\x124\x3\x2\x2"+
		"\x2\x124\x122\x3\x2\x2\x2\x124\x125\x3\x2\x2\x2\x125\x126\x3\x2\x2\x2"+
		"\x126\x128\a>\x2\x2\x127\x129\t\x3\x2\x2\x128\x127\x3\x2\x2\x2\x129\x12A"+
		"\x3\x2\x2\x2\x12A\x128\x3\x2\x2\x2\x12A\x12B\x3\x2\x2\x2\x12B\x12C\x3"+
		"\x2\x2\x2\x12C\x134\a@\x2\x2\x12D\x12F\t\x3\x2\x2\x12E\x12D\x3\x2\x2\x2"+
		"\x12F\x132\x3\x2\x2\x2\x130\x12E\x3\x2\x2\x2\x130\x131\x3\x2\x2\x2\x131"+
		"\x134\x3\x2\x2\x2\x132\x130\x3\x2\x2\x2\x133\x122\x3\x2\x2\x2\x133\x130"+
		"\x3\x2\x2\x2\x134\x135\x3\x2\x2\x2\x135\x136\a+\x2\x2\x136\x137\x3\x2"+
		"\x2\x2\x137\x138\b\v\x2\x2\x138\x16\x3\x2\x2\x2\x139\x13B\t\x4\x2\x2\x13A"+
		"\x139\x3\x2\x2\x2\x13B\x13C\x3\x2\x2\x2\x13C\x13A\x3\x2\x2\x2\x13C\x13D"+
		"\x3\x2\x2\x2\x13D\x13E\x3\x2\x2\x2\x13E\x13F\b\f\x2\x2\x13F\x18\x3\x2"+
		"\x2\x2\x140\x141\a*\x2\x2\x141\x142\aR\x2\x2\x142\x143\ak\x2\x2\x143\x144"+
		"\ar\x2\x2\x144\x145\ag\x2\x2\x145\x146\an\x2\x2\x146\x147\ak\x2\x2\x147"+
		"\x148\ap\x2\x2\x148\x149\ag\x2\x2\x149\x14A\a\x44\x2\x2\x14A\x14B\aw\x2"+
		"\x2\x14B\x14C\ak\x2\x2\x14C\x14D\an\x2\x2\x14D\x14E\a\x66\x2\x2\x14E\x14F"+
		"\ag\x2\x2\x14F\x150\at\x2\x2\x150\x151\a>\x2\x2\x151\x164\x3\x2\x2\x2"+
		"\x152\x154\t\x3\x2\x2\x153\x152\x3\x2\x2\x2\x154\x155\x3\x2\x2\x2\x155"+
		"\x153\x3\x2\x2\x2\x155\x156\x3\x2\x2\x2\x156\x157\x3\x2\x2\x2\x157\x159"+
		"\a>\x2\x2\x158\x15A\t\x3\x2\x2\x159\x158\x3\x2\x2\x2\x15A\x15B\x3\x2\x2"+
		"\x2\x15B\x159\x3\x2\x2\x2\x15B\x15C\x3\x2\x2\x2\x15C\x15D\x3\x2\x2\x2"+
		"\x15D\x165\a@\x2\x2\x15E\x160\t\x3\x2\x2\x15F\x15E\x3\x2\x2\x2\x160\x163"+
		"\x3\x2\x2\x2\x161\x15F\x3\x2\x2\x2\x161\x162\x3\x2\x2\x2\x162\x165\x3"+
		"\x2\x2\x2\x163\x161\x3\x2\x2\x2\x164\x153\x3\x2\x2\x2\x164\x161\x3\x2"+
		"\x2\x2\x165\x166\x3\x2\x2\x2\x166\x167\a.\x2\x2\x167\x168\a\"\x2\x2\x168"+
		"\x17B\x3\x2\x2\x2\x169\x16B\t\x3\x2\x2\x16A\x169\x3\x2\x2\x2\x16B\x16C"+
		"\x3\x2\x2\x2\x16C\x16A\x3\x2\x2\x2\x16C\x16D\x3\x2\x2\x2\x16D\x16E\x3"+
		"\x2\x2\x2\x16E\x170\a>\x2\x2\x16F\x171\t\x3\x2\x2\x170\x16F\x3\x2\x2\x2"+
		"\x171\x172\x3\x2\x2\x2\x172\x170\x3\x2\x2\x2\x172\x173\x3\x2\x2\x2\x173"+
		"\x174\x3\x2\x2\x2\x174\x17C\a@\x2\x2\x175\x177\t\x3\x2\x2\x176\x175\x3"+
		"\x2\x2\x2\x177\x17A\x3\x2\x2\x2\x178\x176\x3\x2\x2\x2\x178\x179\x3\x2"+
		"\x2\x2\x179\x17C\x3\x2\x2\x2\x17A\x178\x3\x2\x2\x2\x17B\x16A\x3\x2\x2"+
		"\x2\x17B\x178\x3\x2\x2\x2\x17C\x17D\x3\x2\x2\x2\x17D\x17E\a@\x2\x2\x17E"+
		"\x17F\a\"\x2\x2\x17F\x192\x3\x2\x2\x2\x180\x182\t\x3\x2\x2\x181\x180\x3"+
		"\x2\x2\x2\x182\x183\x3\x2\x2\x2\x183\x181\x3\x2\x2\x2\x183\x184\x3\x2"+
		"\x2\x2\x184\x185\x3\x2\x2\x2\x185\x187\a>\x2\x2\x186\x188\t\x3\x2\x2\x187"+
		"\x186\x3\x2\x2\x2\x188\x189\x3\x2\x2\x2\x189\x187\x3\x2\x2\x2\x189\x18A"+
		"\x3\x2\x2\x2\x18A\x18B\x3\x2\x2\x2\x18B\x193\a@\x2\x2\x18C\x18E\t\x3\x2"+
		"\x2\x18D\x18C\x3\x2\x2\x2\x18E\x191\x3\x2\x2\x2\x18F\x18D\x3\x2\x2\x2"+
		"\x18F\x190\x3\x2\x2\x2\x190\x193\x3\x2\x2\x2\x191\x18F\x3\x2\x2\x2\x192"+
		"\x181\x3\x2\x2\x2\x192\x18F\x3\x2\x2\x2\x193\x194\x3\x2\x2\x2\x194\x195"+
		"\a+\x2\x2\x195\x196\a\"\x2\x2\x196\x197\a?\x2\x2\x197\x198\a@\x2\x2\x198"+
		"\x199\x3\x2\x2\x2\x199\x19A\b\r\x2\x2\x19A\x1A\x3\x2\x2\x2\x19B\x19C\a"+
		"\x43\x2\x2\x19C\x19D\a\x66\x2\x2\x19D\x19E\a\x66\x2\x2\x19E\x1C\x3\x2"+
		"\x2\x2\x19F\x1A0\a\x43\x2\x2\x1A0\x1A1\a\x66\x2\x2\x1A1\x1A2\a\x66\x2"+
		"\x2\x1A2\x1A3\aK\x2\x2\x1A3\x1A4\ah\x2\x2\x1A4\x1E\x3\x2\x2\x2\x1A5\x1A6"+
		"\a\x43\x2\x2\x1A6\x1A7\a\x66\x2\x2\x1A7\x1A8\a\x66\x2\x2\x1A8\x1A9\aK"+
		"\x2\x2\x1A9\x1AA\ah\x2\x2\x1AA\x1AB\aG\x2\x2\x1AB\x1AC\an\x2\x2\x1AC\x1AD"+
		"\au\x2\x2\x1AD\x1AE\ag\x2\x2\x1AE \x3\x2\x2\x2\x1AF\x1B0\aR\x2\x2\x1B0"+
		"\x1B1\a\x63\x2\x2\x1B1\x1B2\at\x2\x2\x1B2\x1B3\a\x63\x2\x2\x1B3\x1B4\a"+
		"n\x2\x2\x1B4\x1B5\an\x2\x2\x1B5\x1B6\ag\x2\x2\x1B6\x1B7\an\x2\x2\x1B7"+
		"\"\x3\x2\x2\x2\x1B8\x1B9\aK\x2\x2\x1B9\x1BA\ah\x2\x2\x1BA$\x3\x2\x2\x2"+
		"\x1BB\x1BC\a*\x2\x2\x1BC\x1C0\t\x5\x2\x2\x1BD\x1BF\t\x3\x2\x2\x1BE\x1BD"+
		"\x3\x2\x2\x2\x1BF\x1C2\x3\x2\x2\x2\x1C0\x1BE\x3\x2\x2\x2\x1C0\x1C1\x3"+
		"\x2\x2\x2\x1C1\x1C3\x3\x2\x2\x2\x1C2\x1C0\x3\x2\x2\x2\x1C3\x1C4\a+\x2"+
		"\x2\x1C4&\x3\x2\x2\x2\x1C5\x1C6\a*\x2\x2\x1C6\x1CA\t\x5\x2\x2\x1C7\x1C9"+
		"\t\x3\x2\x2\x1C8\x1C7\x3\x2\x2\x2\x1C9\x1CC\x3\x2\x2\x2\x1CA\x1C8\x3\x2"+
		"\x2\x2\x1CA\x1CB\x3\x2\x2\x2\x1CB\x1CD\x3\x2\x2\x2\x1CC\x1CA\x3\x2\x2"+
		"\x2\x1CD\x1CE\a.\x2\x2\x1CE(\x3\x2\x2\x2\x1CF\x1D2\x5-\x17\x2\x1D0\x1D2"+
		"\x5/\x18\x2\x1D1\x1CF\x3\x2\x2\x2\x1D1\x1D0\x3\x2\x2\x2\x1D2*\x3\x2\x2"+
		"\x2\x1D3\x1D6\x5\x31\x19\x2\x1D4\x1D6\x5\x33\x1A\x2\x1D5\x1D3\x3\x2\x2"+
		"\x2\x1D5\x1D4\x3\x2\x2\x2\x1D6,\x3\x2\x2\x2\x1D7\x1D9\a>\x2\x2\x1D8\x1DA"+
		"\t\x3\x2\x2\x1D9\x1D8\x3\x2\x2\x2\x1DA\x1DB\x3\x2\x2\x2\x1DB\x1D9\x3\x2"+
		"\x2\x2\x1DB\x1DC\x3\x2\x2\x2\x1DC\x1DD\x3\x2\x2\x2\x1DD\x1DF\a>\x2\x2"+
		"\x1DE\x1E0\t\x3\x2\x2\x1DF\x1DE\x3\x2\x2\x2\x1E0\x1E1\x3\x2\x2\x2\x1E1"+
		"\x1DF\x3\x2\x2\x2\x1E1\x1E2\x3\x2\x2\x2\x1E2\x1E3\x3\x2\x2\x2\x1E3\x1E4"+
		"\a@\x2\x2\x1E4\x1E5\a@\x2\x2\x1E5.\x3\x2\x2\x2\x1E6\x1E8\a>\x2\x2\x1E7"+
		"\x1E9\t\x3\x2\x2\x1E8\x1E7\x3\x2\x2\x2\x1E9\x1EA\x3\x2\x2\x2\x1EA\x1E8"+
		"\x3\x2\x2\x2\x1EA\x1EB\x3\x2\x2\x2\x1EB\x1EC\x3\x2\x2\x2\x1EC\x1ED\a@"+
		"\x2\x2\x1ED\x30\x3\x2\x2\x2\x1EE\x1F0\a>\x2\x2\x1EF\x1F1\t\x3\x2\x2\x1F0"+
		"\x1EF\x3\x2\x2\x2\x1F1\x1F2\x3\x2\x2\x2\x1F2\x1F0\x3\x2\x2\x2\x1F2\x1F3"+
		"\x3\x2\x2\x2\x1F3\x1F4\x3\x2\x2\x2\x1F4\x1F6\a>\x2\x2\x1F5\x1F7\t\x3\x2"+
		"\x2\x1F6\x1F5\x3\x2\x2\x2\x1F7\x1F8\x3\x2\x2\x2\x1F8\x1F6\x3\x2\x2\x2"+
		"\x1F8\x1F9\x3\x2\x2\x2\x1F9\x1FA\x3\x2\x2\x2\x1FA\x1FB\a@\x2\x2\x1FB\x1FC"+
		"\a.\x2\x2\x1FC\x1FD\a\"\x2\x2\x1FD\x1FF\x3\x2\x2\x2\x1FE\x200\t\x3\x2"+
		"\x2\x1FF\x1FE\x3\x2\x2\x2\x200\x201\x3\x2\x2\x2\x201\x1FF\x3\x2\x2\x2"+
		"\x201\x202\x3\x2\x2\x2\x202\x203\x3\x2\x2\x2\x203\x205\a>\x2\x2\x204\x206"+
		"\t\x3\x2\x2\x205\x204\x3\x2\x2\x2\x206\x207\x3\x2\x2\x2\x207\x205\x3\x2"+
		"\x2\x2\x207\x208\x3\x2\x2\x2\x208\x209\x3\x2\x2\x2\x209\x20A\a@\x2\x2"+
		"\x20A\x20B\a@\x2\x2\x20B\x32\x3\x2\x2\x2\x20C\x20E\a>\x2\x2\x20D\x20F"+
		"\t\x3\x2\x2\x20E\x20D\x3\x2\x2\x2\x20F\x210\x3\x2\x2\x2\x210\x20E\x3\x2"+
		"\x2\x2\x210\x211\x3\x2\x2\x2\x211\x212\x3\x2\x2\x2\x212\x213\a.\x2\x2"+
		"\x213\x214\a\"\x2\x2\x214\x216\x3\x2\x2\x2\x215\x217\t\x3\x2\x2\x216\x215"+
		"\x3\x2\x2\x2\x217\x218\x3\x2\x2\x2\x218\x216\x3\x2\x2\x2\x218\x219\x3"+
		"\x2\x2\x2\x219\x21A\x3\x2\x2\x2\x21A\x21B\a@\x2\x2\x21B\x34\x3\x2\x2\x2"+
		"\x21C\x21E\t\x3\x2\x2\x21D\x21C\x3\x2\x2\x2\x21E\x21F\x3\x2\x2\x2\x21F"+
		"\x21D\x3\x2\x2\x2\x21F\x220\x3\x2\x2\x2\x220\x36\x3\x2\x2\x2\x221\x222"+
		"\a*\x2\x2\x222\x38\x3\x2\x2\x2\x223\x224\a+\x2\x2\x224:\x3\x2\x2\x2/\x2"+
		"\xC4\xE0\xE6\xEC\xEF\xF7\xFD\x103\x106\x10D\x113\x119\x11C\x124\x12A\x130"+
		"\x133\x13C\x155\x15B\x161\x164\x16C\x172\x178\x17B\x183\x189\x18F\x192"+
		"\x1C0\x1CA\x1D1\x1D5\x1DB\x1E1\x1EA\x1F2\x1F8\x201\x207\x210\x218\x21F"+
		"\x3\b\x2\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace PowerPipe.Visualization.Antlr