# KodeopgaveFiskeristyrelse

## Generelle Mangler (and how i would've gone about it, given more time..)

Jeg nåede at implementere de fleste funktionelle krav - det eneste jeg ikke virkelig blev færdigt med var
 * at være i stand til at definere specifikke tidsrum og vise de entrie's som falder ind i disse
 * at være i stand til at sortere baseret på f.eks. bestemte arter eller lokationer etc. 

Den første af disse fejlede på problemer med at konvertere SQL's DateTime format og C#'s/Windows Form's DateTime format korrekt frem og tilbage og bruge det til at lave sammenligninger i databasen (på trods af mange mange forskellige og ihærdige forsøg). Den anden ville i og for sig have været let nok, men jeg løb tør for tid. SQL databaser understøtter en syntaks som tillader det at sende flere forskellige querie's afsted samtidigt, ved kun at åbne én forbindelse til databasen/serveren (performance!!!). Det kunne f.eks. have været at hente "lister" med diverse forskellige distinct'e arter, lokationer etc etc. Disse kunne så igen være blevet brugt til at populate dropdown-menuer i brugergrænseoverfladen på dynamisk vis, pogrammatically. Not really incredibly difficult, but I ran out of time. 

## Testing

Jeg ville have elsket at have haft tid til at teste at CRUD metoderne virker (Create/Insert, Read, Update, Delete) ved at lave nogle veldokumenterede unit tests. Arrange, Act, Assert. I mange forskellige kompleksitetsgrader og kombinationer. I do know how, I did not have the time. It is important. And it is currently missing. As it is, og uden rent faktisk virkelig at have testet det, så er jeg rimeligvis sikker på at min kode er (tilnærmelsesvist) korrekt. Men ja. 

## Vanilla SQL vs. more modern approaches

I dette tilfælde gik jeg med en fremgangsmåde baserende på at skrive godt gamle SQL queries i string-format direkte ind i koden. As such, så ville jeg have foretrukket at arbejde med mere moderne alternativer, såsom LINQ og/eller extension methods, som tillader det nærmest at kode direkte i databasen og uden nogensinde virkelig at skrive en linje SQL. Omend LINQ har nogle ligheder. 
Endnu et alternativ, og min personlige klare favorit, er MS's nyeste tiltag - the Code-First approach Entity Framework Core, gathering all of the best features of all worlds. However, it was arguably way overkill for the scope of this exercise, as it takes some time setting up (correctly). 

## Overtænkning, normalisering/normal-former og billige løsninger..

Jeg gik faktisk, som det første igang med at skrive et script til "manuelt" at parse CSV filen til en database.. kun for så at finde ud af at databasen i sig selv skabte problemer. Pointen er, at jeg tænkte, at siden det var SQL og en relationel database, så ville det give mening at opdele data'en lidt i forskellige tabeller. Primary keys, foreign keys, various kinds of joins, unions etc. Men så løb jeg tør for tid, og endte med bare direkte at importere CSV filen til databasen og tage den derfra. Either way, det var endnu en ting som kostede dyrebar tid, og ellers sikkert havde givet mening.. Også fordi, at jeg flere gange havde gjort det for, til forskellige skole projekter, og som sådan også var kommet rimeligvist tæt på at have fået det til at fungere. Jeg har "vedlagt" scriptet her i repositoriet bare for sjovt, hvis der sku