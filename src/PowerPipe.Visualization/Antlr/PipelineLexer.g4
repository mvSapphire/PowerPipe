lexer grammar PipelineLexer;

// Temporary skipping these
ONERRORRETRY:    '.OnError(PipelineStepErrorHandling.Retry)' -> skip;
ONERRORSUPPRESS: '.OnError(PipelineStepErrorHandling.Suppress)' -> skip;
COMPENSATE:      '.CompensateWith' DATA '()' -> skip;

LEFTARROW:    '<' -> skip;
RIGHTARROW:   '>' -> skip;
EMPTYPAR:     '()' -> skip;
COMA:         ',' -> skip;
DOT:          '.' -> skip;
WS:           [ \t\r\n]+ -> skip;

NEWBUIDLER:   'new PipelineBuilder<' (([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>' | [a-zA-Z0-9_]*)) ', '
              (([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>' | [a-zA-Z0-9_]*)) '>' '('
              (([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>' | [a-zA-Z0-9_]*)) ', '
              (([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>' | [a-zA-Z0-9_]*)) ')' -> skip;

LAMBDANAME:   [a-z]+ -> skip;
LAMBDA:       '(PipelineBuilder<' ([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>' | [a-zA-Z0-9_]*) ', '
              ([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>' | [a-zA-Z0-9_]*) '> '
              ([a-zA-Z0-9_]+'<'[a-zA-Z0-9_]+'>' | [a-zA-Z0-9_]*) ') =>' -> skip;

ADD:          'Add';
ADDIF:        'AddIf';
ADDIFELSE:    'AddIfElse';
PARALLEL:     'Parallel';
IF:           'If';
PREDICATE:    '(' [a-zA-Z][a-zA-Z0-9_]* ')';
OPENPREDICATE: '(' [a-zA-Z][a-zA-Z0-9_]* ',';
DATA:         (STEPWITHGENERIC | STEPWITHOUTGENERIC);
DATA2:        (TWOSTEPSWITHGENERIC | TWOSTEPSWITHOUTGENERIC);

STEPWITHGENERIC:        '<' [A-Za-z_0-9]+ '<' [A-Za-z_0-9]+ '>' '>';
STEPWITHOUTGENERIC:     '<' [A-Za-z_0-9]+ '>';

TWOSTEPSWITHGENERIC:    '<' [A-Za-z_0-9]+ '<' [A-Za-z_0-9]+ '>' ', ' [A-Za-z_0-9]+ '<' [A-Za-z_0-9]+ '>' '>';
TWOSTEPSWITHOUTGENERIC: '<' [A-Za-z_0-9]+ ', ' [A-Za-z_0-9]+ '>';

ANYTEXT:      ('A'..'Z' | 'a'..'z' | '0'..'9' | '_')+;

OPENPAR:      '(';
CLOSEPAR:     ')';
