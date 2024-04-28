# CGT Conto giudiziale targhe

Il progetto nasce per ottenere un rendiconto agevole sull’utilizzo delle targhe all’ufficio della motorizzazione. 
Il sistema attraverso una rappresentazione a tabella mostra nella prima colonna tutte le giacenze rimanenti per ogni tipo di targa proveniente dall’anno precedente e mese per mese permette l’inserimento e la modifica di tutti i carichi e scarichi del modello di targa specifico. 
Nelle colonne finali della rappresentazione ci mostra i totali, parziali e le rimanenze per ogni tipo di targa inserita.

FLUSSO DI BASE CGT
-	Visualizzazione index del sistema - pagina di benvenuto che presenta un menu in alto e tre possibili aree di accesso:
-	Visualizza: mostra il resoconto CGT annuale aggiornato all’ultima modifica.
-	Modifica: accesso consentito previa registrazione e login, abilita le funzioni di creazione, modifica, dettaglio e cancella sul resoconto CGT che agiscono direttamente su tutti i record della riga selezionata.
-	Login: tramite il login permette l’accesso all’area di “Modifica”.

-	Riconoscimento utente: il sistema riconosce l’utente autenticato e ne conferisce i privilegi associati.

-	Visualizzazione del resoconto: una volta effettuato il riconoscimento tramite il login avremo accesso alla visualizzazione del resoconto CGT ed abilitato i privilegi dell’utente amministratore.


ESTENSIONI E FLUSSI ALTERNATIVI
-	Funzione di stampa: presente all’interno dell’area “Visualizza”, genera un file pdf di tutti i record inseriti formattato per una stampa A4.

-	Creazione dei Ruoli di accesso: previsti 3 tipi di privilegi collegati a 3 ruoli per un differente utilizzo del sistema: amministratore, utente motorizzazione ed utente agenzia. 
I seguenti ruoli avranno diverse interfacce di visualizzazione in base alle esigenze di inserimento nel resoconto annuale, in più l’utente agenzia avrà una policy di modifica e visualizzazione inerente unicamente al proprio lavoro ed ai propri inserimenti.

-	Creazioni di filtri dati per le visualizzazioni e gli inserimenti: creazioni di una vista e relativo controllo di filtro per data in modo da poter visualizzare più anni, mesi e di conseguenza permettere la modifica e l’inserimento nelle viste collegate a tutti gli account in base ai privilegi conferiti.


METODI E TECNICHE UTILIZZATI
Implementata una WEBAPP ModelViewController ASP.NETCore 7.0 con VisualStudio2022 collegata ad un gruppo di risorse Azure nominato Targhe che comprende SQL server, DB e servizio app. 

Utilizzate le seguenti librerie e packpage:
Bootstrap e jquery 
Microsoft EntityFrameworkCore
Microsoft EntityFrameworkCore.Sqlite
Microsoft EntityFrameworkCore.sqlServer
Microsoft EntityFrameworkCore.Tools
AspNetCore Identity
AspNetCore Identity.UI
AspNetCore Identity.EntityFrameworkCore
MicrosoftVisualstudioWebCodeGeneration

