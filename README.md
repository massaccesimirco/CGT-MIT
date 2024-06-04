# CGT Conto giudiziale targhe

Il progetto nasce per ottenere un rendiconto agevole sull’utilizzo delle targhe all’ufficio della motorizzazione. 
Il sistema attraverso una rappresentazione a tabella mostra nella prima colonna tutte le giacenze rimanenti per ogni tipo di targa proveniente dall’anno precedente e mese per mese permette l’inserimento e la modifica di tutti i carichi e scarichi del modello di targa specifico. 
Nelle colonne finali della rappresentazione ci mostra i totali, parziali e le rimanenze per ogni tipo di targa inserita.

FLUSSO DI BASE CGT

Funzionamento del Sistema

Modello (Model)
   - La tabella "Items" è il fulcro dei dati inseriti e gestiti. Questa tabella contiene informazioni relative agli elementi che vengono monitorati e gestiti dagli utenti.
   - Un'altra tabella gestisce gli utenti e i ruoli, integrata con Microsoft Identity Framework.
   - Dati annuali: gestione di carico e scarico mensile, giacenza iniziale, tipo di targa, rimanenza da riportare all'anno successivo, totale di carico, totale di scarico e totale finale.

Vista (View)
   - La vista è responsabile della presentazione dei dati agli utenti. Esistono diverse visualizzazioni per gestire, visualizzare e inserire dati.
   - Le viste sono personalizzate in base al ruolo dell'utente, mostrando solo le informazioni e le funzionalità pertinenti.
   - Visualizzazione di anni precedenti con tutti i dettagli dei dati inclusi.

Controller
   - I controller gestiscono le richieste degli utenti e operano sul modello per fornire le viste appropriate.
   - Ogni sezione (Gestione, Visualizzazione, Inserimento) è controllata da specifici controller che implementano la logica di business necessaria.
   - Gestione della chiusura dell'anno e la generazione di report annuali basati sui dati passati.

Attori del Sistema

Admin
   - Ha accesso completo a tutte le funzionalità.
   - Può creare, modificare e chiudere un anno di dati.
   - Gestisce gli utenti e i loro ruoli.
   - Visualizzazione di tutti gli anni precedenti con i dettagli dei dati.

Ufficio
   - Può inserire e visualizzare i dati.
   - Ha accesso completo alla vista di inserimento e a tutte le funzioni di visualizzazione.
   - Visualizzazione di tutti gli anni precedenti con i dettagli dei dati.

Agenzia
   - Può inserire dati e visualizzare solo gli inserimenti effettuati dal singolo utente.
   - Visualizzazione dei propri dati per tutti gli anni precedenti.

Casi d'Uso Specifici

Admin
   - Gestione Anno Dati: Creazione, modifica e chiusura di un anno di dati.
   - Gestione Utenti: Creazione, modifica e assegnazione dei ruoli agli utenti.
   - Visualizzazione Completa: Accesso a tutti i dati nella tabella "Items" con filtri per nome, data, anno e mese, e visualizzazione dei dati degli anni precedenti.
   - Generazione PDF: Creazione di un PDF della tabella finale utilizzando Rotativa.
   - Visualizzazione Annuale: Accesso ai dati dettagliati di ogni anno chiuso, inclusi carico, scarico, giacenza iniziale, tipo di targa, rimanenza, totale di carico, totale di scarico e totale finale.
   - Generazione Report Annuale: Creazione di relazioni annuali basate sui dati storici.

2. Ufficio
   - Inserimento Dati: Inserimento di nuovi dati nella tabella "Items".
   - Visualizzazione Dati:Accesso a tutti i dati nella tabella "Items" con filtri per nome, data, anno e mese, e visualizzazione dei dati degli anni precedenti.
   - Generazione PDF:Creazione di un PDF della tabella finale utilizzando Rotativa (se permesso).
   - Visualizzazione Annuale:Accesso ai dati dettagliati di ogni anno chiuso, inclusi carico, scarico, giacenza iniziale, tipo di targa, rimanenza, totale di carico, totale di scarico e totale finale.

3. Agenzia
   - Inserimento Dati: Inserimento di nuovi dati nella tabella "Items".
   - Visualizzazione Personale: Accesso solo ai dati inseriti dall'utente corrente nella tabella "Items".
   - Generazione PDF: Creazione di un PDF della tabella finale con i propri dati utilizzando Rotativa (se permesso).
   - Visualizzazione Annuale Personale: Accesso ai propri dati dettagliati di ogni anno chiuso


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

