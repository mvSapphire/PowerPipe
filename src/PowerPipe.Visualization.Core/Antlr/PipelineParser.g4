parser grammar PipelineParser;

options { tokenVocab = PipelineLexer; }

start: step+;

step: addStep | addIfStep | addIfElseStep | ifStep | parallelStep;

addStep: ADD DATA;
addIfStep: ADDIF DATA PREDICATE;
addIfElseStep: ADDIFELSE DATA DATA PREDICATE;
ifStep: IF OPENPAR ANYTEXT step+ CLOSEPAR;
parallelStep: PARALLEL OPENPAR step+ CLOSEPAR;
