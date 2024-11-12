grammar SpaceBattleParser;
options { tokenVocab=SpaceBattleLexer; }

game: command+ ;

command
    : playerCommand
    | gameCommand
    | commandExtention
    | produceCommand
    ;

playerCommand
    : 'Player' ID 'can' 'command' TYPE ID action (',' action)* 
    ;

gameCommand
    : 'Game' 'can' 'command' gameAction 
    ;

commandExtention
    : RULE 'includes' (RULE | produceCommand) (',' (RULE | produceCommand))*
    ;

produceCommand
    : 'Produce' ID 'typeof' TYPE
    ;

gameAction
    : timedGameAction
    | manipulateGameAction
    | RULE (',' RULE)*
    ;

timedGameAction
    : RULE ('every' | 'after') INT 'seconds'
    ;
    
manipulateGameAction
    : RULE 'to' TYPE ID
    ;

action
    : RULE
    ;
