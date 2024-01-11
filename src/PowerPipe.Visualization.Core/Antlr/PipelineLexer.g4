lexer grammar PipelineLexer;

NEWBUIDLER:   'new PipelineBuilder<' (([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>'|[a-zA-Z0-9_]*)) ', ' (([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>'|[a-zA-Z0-9_]*)) '>' '(' (([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>'|[a-zA-Z0-9_]*)) ', ' (([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>'|[a-zA-Z0-9_]*)) ')' -> skip;

LAMBDANAME:   [a-z]+ -> skip;
LAMBDA:       '(PipelineBuilder<' ([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>'|[a-zA-Z0-9_]*) ', ' ([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>'|[a-zA-Z0-9_]*) '> ' ([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>'|[a-zA-Z0-9_]*) ') =>' -> skip;


ADD:          'Add';
ADDIF:        'AddIf';
ADDIFELSE:    'AddIfElse';
PARALLEL:     'Parallel';
IF:           'If';
PREDICATE:    '(' [a-zA-Z][a-zA-Z0-9_]* ')';
ANYTEXT:         ('A'..'Z' | 'a'..'z' | '0'..'9' | '_')+;
DATA: (STEPWITHGENERIC | STEPWITHOUTGENERIC);

STEPWITHGENERIC : '<' [A-Za-z_0-9]* '<' [A-Za-z_0-9]+ '>' '>';
STEPWITHOUTGENERIC : '<' [A-Za-z_0-9]* '>';

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
