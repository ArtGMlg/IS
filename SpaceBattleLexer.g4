lexer grammar SpaceBattleLexer;

PLAYER: 'Player';
GAME: 'Game';
PRODUCE: 'Produce';
CAN: 'can';
COMMAND: 'command';
SPACESHIP: 'Spaceship';
INCLUDES: 'includes';
TYPEOF: 'typeof';
TO: 'to';
EVERY: 'every';
AFTER: 'after';
SECONDS: 'seconds';

COMMA: ',';
INT : [0-9]+ ;
RULE: [a-zA-Z]+ 'Command';
TYPE: [a-zA-Z]+ 'Type';
ID: [a-zA-Z_][a-zA-Z0-9_]*;
WS: [ \t\r\n]+ -> skip;
