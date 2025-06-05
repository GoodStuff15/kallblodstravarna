## Kallblodstravarnas Resort
   Ett projektarbete i C# .NET för SUT24.

   Entity Framework Core
   MySQL
   OpenAPI
   Postman
   MSTest

   **Medverkande:**
   Egzon "Frontend" Jashari
   Shokran B
   ICA-Johan
   IGustav<T>
   
   ### - Projektbeskrivning och arkitekturöversikt

    Projektet består av ett API för att boka boende och aktiviteter på en vacker paradisresort. 

    Huvudidén bakom valet av bokningssystem är att ha möjlighet att designa ett flexibelt system som enkelt kan utökas med ytterligare
    tillval, allt eftersom resorten växer.

    Projektets solution består av fem projekt: 
    
    * ResortApi, som innehåller businesslogiken.
    * Resortlibrary, som innehåller modellerna som används i projektet samt builders för dessa.
    * Resortdtos, som innehåller enbart Data transfer objects
    * Tests, som innehåller enhetstester för merparten av applikationen
    * Authtests, som innehåller tester för applikationens implementering av auktorisering

    Backenden är uppbyggt främst kring tre modeller - Customer, Booking och Accomodation. I dessa två samlas information om
    vilka rum som finns, vilka som är bokade, vem som har bokat vad och hur länge, etc.

    Det finns även modeller för AccomodationType (vilken typ av boende är ett rum), Guest (information om gäster som inte är beställare),
    PriceChanges (rabatter med mera), AdditionalOption (tillval till boendet), Accessibilities (Anpassningar för t.ex. handikapp mm.) samt
    User (för autentisering).

    Applikationen tillämpar ett service-repository pattern, där businesslogiken är separerad från API-requests och databas-operationer.

    Informations/dataflödet ser ut på följande sätt:

    User <-> Api Controller <-> Service <-> Repository <-> Databas

    I Controller-lagret sker mottagande och svarande på användares input
    I Service-lagret sker validering och konvertering (mellan dto och objekt)
    I Repository-lagret sker databashantering.

    
  ###  - Dokumentation av API-endpoints

  "https://github.com/GoodStuff15/kallblodstravarna/blob/main/API%20Documentation.md"
    
  ###  - Beskrivning av teststrategi och resultat

   Vår teststrategi har innefattat en omfattande testdriven utveckling under uppbyggnadsfasen av applikationen.
   Vid framtagning av i stort sett alla klasser har enhetstestning skett i MsTest för att sen gå vidare och skapa
   klasserna efter testresultaten, enligt TDD. 
   
   Vid testning av relationer mellan olika delar av applikationen (se dataflödet ovan) så har detta frångåtts till viss del. 
   Testning har i sådana fall skett i efterhand med hjälp av Entity Framework Cores inbyggda In Memory Database samt 
   med mock-biblioteket Moq.

   Mitt under utvecklingen refaktorerade vi vårt class library för att använda builder classes istället för
   factories. Detta innebar att vi också skrev om de tester som redan skapats. 
   Efter det refaktorerade vi även API:et till att använda ett Service-lager. Service-funktionerna har testats, 
   men på grund av tidsbrist inte lika omfattande som de tidigare framtagna delarna av systemet. 

   Sammanfattningsvis har vi en solid testgrund att stå på och många av delarna av applikationen står på egna ben - 
   det vill säga att de dels följer single responsibility, och dels är så pass gediget testade att de enkelt
   skulle kunna portas till andra applikationer.
