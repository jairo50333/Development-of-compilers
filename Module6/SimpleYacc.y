%{
// Эти объявления добавляются в класс GPPGParser, представляющий собой парсер, генерируемый системой gppg
    public BlockNode root; // Корневой узел синтаксического дерева 
    public Parser(AbstractScanner<ValueType, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%union { 
			public double dVal; 
			public int iVal; 
			public string sVal; 
			public Node nVal;
			public ExprNode eVal;
			public StatementNode stVal;
			public BlockNode blVal;
       }

%using ProgramTree;

%namespace SimpleParser

%token BEGIN END CYCLE ASSIGN SEMICOLON WHILE DO REPEAT UNTIL FOR TO WRITE IF THEN ELSE VAR PLUS MINUS MULT DIV LEFT_BRACKET RIGHT_BRACKET COMMA
       
%token <iVal> INUM 
%token <dVal> RNUM 
%token <sVal> ID

%type <eVal> expr ident T F 
%type <stVal> assign statement cycle while repeat for write if var varlist
%type <blVal> stlist block 

%%

progr   : block { root = $1; }
		;

stlist	: statement 
			{ 
				$$ = new BlockNode($1); 
			}
		| stlist SEMICOLON statement 
			{ 
				$1.Add($3); 
				$$ = $1; 
			}
		;

statement: assign { $$ = $1; }
		| block   { $$ = $1; }
		| cycle   { $$ = $1; }
		| while   { $$ = $1; } 
		| repeat  { $$ = $1; } 
		| for	  { $$ = $1; } 
		| write	  { $$ = $1; } 
		| if	  { $$ = $1; } 
		| var     { $$ = $1; } 
	;

ident 	: ID { $$ = new IdNode($1); }	
		;
	
assign 	: ident ASSIGN expr { $$ = new AssignNode($1 as IdNode, $3); }
		;

expr	: expr PLUS T { $$ = new BinaryNode($1, $3, '+'); }
		| expr MINUS T { $$ = new BinaryNode($1, $3, '-'); }
		| T { $$ = $1; }
		;
		
T 		: T MULT F { $$ = new BinaryNode($1, $3, '*'); }
		| T DIV F { $$ = new BinaryNode($1, $3, '/'); }
		| F { $$ = $1; }
		;
		
F 		: ident  { $$ = $1 as IdNode; }
		| INUM { $$ = new IntNumNode($1); }
		| LEFT_BRACKET expr RIGHT_BRACKET { $$ = $2; }
		; 

block	: BEGIN stlist END { $$ = $2; }
		;

cycle	: CYCLE expr statement { $$ = new CycleNode($2, $3); }
		;

while   : WHILE expr DO statement { $$ = new WhileNode($2, $4); }
	    ;		

repeat : REPEAT stlist UNTIL expr { $$ = new RepeatNode($2, $4); } 	  
	   ;

for    : FOR ident ASSIGN expr TO expr DO statement { $$ = new ForNode($2 as IdNode, $4, $6, $8); }
       ;	   

write   : WRITE LEFT_BRACKET expr RIGHT_BRACKET { $$ = new WriteNode($3); }
        ;

if      : IF expr THEN statement ELSE statement { $$ = new IfNode($2, $4, $6); }
        | IF expr THEN statement { $$ = new IfNode($2, $4); }      
        ;

varlist	: ident { $$ = new VarDefNode($1 as IdNode); }
		| varlist COMMA ident
            { 
                ($1 as VarDefNode).Add($3 as IdNode); 
                $$ = $1;
            }
		;

var     : VAR varlist SEMICOLON;
	   	
%%

