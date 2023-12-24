lexer grammar PipelineLexer;

NEWBUIDLER:   'new PipelineBuilder<' DATA ', ' DATA '>' '(' DATA ', ' DATA ')' -> skip;
LAMBDANAME:   [a-z]+ -> skip;
LAMBDA:       '(PipelineBuilder<' DATA ', ' DATA '> ' DATA ') =>' -> skip;

ADD:          'Add';
ADDIF:        'AddIf';
ADDIFELSE:    'AddIfElse';
PARALLEL:     'Parallel';
IF:           'If';
PREDICATE:    '(' IDENTIFIER ')';
DATA:         ('A'..'Z' | 'a'..'z' | '0'..'9' | '_')+;
IDENTIFIER:   [a-zA-Z][a-zA-Z0-9_]*;

LEFTARROW:    '<' -> skip;
RIGHTARROW:   '>' -> skip;
OPENPAR:      '(';
CLOSEPAR:     ')';
EMPTYPAR:     '()' -> skip;
COMA:         ',' -> skip;
DOT:          '.' -> skip;
WS:           [ \t\r\n]+ -> skip ;


// temporary skipping this
ONERRORRETRY:    '.OnError(PipelineStepErrorHandling.Retry)' -> skip;
ONERRORSUPPRESS: '.OnError(PipelineStepErrorHandling.Suppress)' -> skip;
COMPENSATE:      '.CompensateWith<' DATA '>' '()' -> skip;
