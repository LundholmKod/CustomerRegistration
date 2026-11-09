Välkommen till min konsolapplikation som innehåller grundläggande funktionalitet för att registera ett kundkonto.
Applikationen är byggd i C#. 
Programmet kontrollerar obligatoriska uppgifter, e-postformat och dubbletter. 
Programmet innehåller även funktion som gör det möjligt att avbryta registreringen.

**För att köra programmet krävs:**
- Visual Studio med arbetsbelastningen .NET desktop development.
- .NET SDK som matchar projektets TargetFramework i filen CustomerRegistration.csproj.

## Som här kör du programmet i Visual Studio
1. Klona repositoryt eller ladda ner och packa upp det.
2. Öppna lösningsfilen (.sln eller .slnx) i Visual Studio.
3. I solution explorer: högerklicka på projektet CustomerRegistration och välj **Set as Startup Project**.
4. Starta programmet med **Ctrl + F5**.

När du startat programmet så kommer du till huvudmenyn som innehåller tre alternativ:

1. **Registrera kundkonto** – ange namn, organisationsnummer, e-postadress och telefonnummer.
2. **Visa sparade kunder** – visar kunderna som registrerats under körningen.
3. **Avsluta** – stänger programmet.

Bekräfta registrering av ny kund genom "j" och ENTER . Skriv /avbryt i valfritt registreringsfält för att avbryta utan att spara.

Vid saknade uppgifter, ogiltigt e-postformat eller en redan registrerad e-postadress visas ett felmeddelande. Därefter kan du försöka igen eller avsluta registreringen.

Kunderna lagras endast i minnet och försvinner när programmet avslutas

## Progammet innehåller även automatiserade tester som kan köras genom att:

1. Öppna **Test → Test Explorer** i Visual Studio.
2. Klicka på **Run All Tests**.

Testerna använder xUnit och kontrollerar registrering, obligatoriska fält, e-postformat och dubbletter
