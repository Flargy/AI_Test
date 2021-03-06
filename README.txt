Grupp 14
Marcus Lindqvist
Niclas Älmeby
www.github.com/Flargy/AI_Test

--------------------

I detta projekt så har vi skapat en AI vars uppgift är att hitta en spelaren som gömmer sig.
OBS "Spelaren" är ingen riktigt spelare i detta fall utan är bara ett statiskt objekt.

Detta projekt är ett Unity-projekt och kräver därför unity att kunna öppna.
Den version av Unity som projektet är skapat i är 2019.2.9, dock så bör det vara möjligt att öppna med en senare version av Unity.

För att kunna öppna projektet så öppna mappen "AI_Testing_xNode" genom Unity.
Sedan för att testa projeket så behövs behöver man sedan klicka på spela-knappen (▶).

--------------------

De olika geomertiska formerna som är utplacerade på kartan är gömställen.
Spelaren är en grön kub.
Ovanför gömställena så finns det två siffor
	Den första siffran visar hur stor chans AI: har att välja detta gömställe.
	Den andra siffran visar hur stor sanorlikhet spelaren har att välja att gömma sig vid detta gömställe.
De gröna sifforna är platsernas (inte att förväxlas med gömställenas) sanorlikhet att väljas.
Det går att se storleken på platserna genom att klicka på "gizmos" knappen till höger ovanför spelvyn.
Detta går också att göra i scenvyn. 

Det går att ändra hastigheten på simulationen genom att dra slidern som finns nere i vänstra hörnet i spelvyn.

Simulationen forstätter hur länge som helst. När AI:n hittar spelaren så uppdateras alla värden, spelaren väljer
ett nytt gömställe och simulationen börjar om.

--------------------

Problembeskrivningen för denna uppgift är hur man kan skapa en AI agent som använder sig av ett behavior tree och decision tree som arbetar tillsammans.
Information om behavior tree hittades online då kurslitteraturen inte tar upp det.
Decision tree informaiton togs från kurslitteraturen från följande stycke:
Russel and Norvig (2010). Artificial Intelligence: A Modern Approach, 3rd edition. Chapter 18 "Learning from examples", Section 3" p. 697