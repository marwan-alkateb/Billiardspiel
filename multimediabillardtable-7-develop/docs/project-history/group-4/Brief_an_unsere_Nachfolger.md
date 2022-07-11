# Brief an unsere Nachfolger

Hallo ihr Lieben,

ihr dürft euch unsere glücklichen Nachfolger nennen. 😉\
Um euch den Einstieg in das Projekt zu erleichtern und euch ein Paar Tipps zu
geben haben wir diesen Brief verfasst. Wir hoffen, dass wir dabei erfolgreich
sind.

## Zu aller erst
Um das Repo zu klonen, nutzt bitte folgenden Befehl:
```
git clone --recurse-submodules https://code.fbi.h-da.de/mmbt/ss2020/multimediabillardtable-4.1.git (<Zielordner>)
```
Wichtig ist das Argument `--recurse-submodules`.

## Inhalt

* [Wie ist das Projekt aufgebaut?](#wie-ist-das-projekt-aufgebaut)
* [Was gibt es noch zu tun?](#was-gibt-es-noch-zu-tun)
* [Was viel uns schwer?](#was-viel-uns-schwer)
* [Tipps für das Team](#tipps-f%c3%bcr-das-team)
* [Tipps für Scrum Master und Product Owner](#tipps-f%c3%bcr-scrum-master-und-product-owner)
  * [Product Owner](#product-owner)
  * [Scrum Master](#scrum-master)


## Wie ist das Projekt aufgebaut?

Es gibt einige einzelne Projekte, die zusammen das Gesamtsystem ergeben.
Dadurch kann gut parallel in mehreren Teams am Projekt gearbeitet werden und
das Gesamtsystem einfacher erweitert werden.

Das Wichtigste über den Aufbau des Projekts ist in der README Datei im
Hauptverzeichnis festgehalten.

Weitere Details findet ihr in den einzelnen Verzeichnissen der Systeme und dem
*doc* Verzeichnis. Auch im *doc/old* Verzeichnis finden sich einige Nützliche
Informationen. Diese können allerdings veraltet sein.

## Was gibt es noch zu tun?

Natürlich mehr, als wir hier aufzählen können. Ihr sollt ja auch eigene Ideen
entwickeln. Die einzelnen Teams haben aber in ihren Systemdokumentationen einige
Punkte festgehalten, bei denen ihr ansetzen könnt.

Hier noch ein paar systemübergreifende Aufgaben:

* Eine Vorschau der Schussbahn, mithilfe eines getrackten Queues. Hier kann der
  Spieler bereits vor dem Schießen sehen, wie die Kugel rollen wird.
* Ein gutes Kabelmanagement.
* Ein eigenes Netzteil. Anstelle des Labornetzteils.
* Weiter Spielarten (z. B. komplett virtuelle Bälle, die mit dem Queue, der
  getrackt wird, angespielt werden. Dabei können sich virtuelle Hindernisse etc.
  auf dem Spielfeld befinden.)
* Eine geeignete Eingabemöglichkeit für die Menüs. Überlegungen hier waren
  Hardwareknöpfe oder einen getrackten Ball zur Auswahl im Menü.

## Was viel uns schwer?

Besonders zu Beginn haben wir uns mit einem sehr unaufgeräumten Repository
schwergetan. Es gab zwar auch Dokumentation, diese fiel aber etwas dürftig aus.
Wir hoffen, dass wir in diesen Punkten einiges besser machen und versuchen das
Projekt gut zu strukturieren und zu dokumentieren.
Eine hohe Einarbeitungszeit wird aber wahrscheinlich bleiben.
Achtet deshalb darauf, in jedem Team mindestens einen Experten zu haben, der bei
Problemen helfen kann.

## Tipps für das Team

Bei uns hat es geholfen, schnell ein Team zu wählen. Die ohnehin hohe
Einarbeitungszeit kann damit gekürzt werden. Ein späterer Teamwechsel ist
möglich und sogar gut, falls andere Teams irgendwann Hilfe benötigen.\
Es ist auch zu empfehlen, frühzeitig Guidelines für eure Zusammenarbeit
aufzustellen. Diese können beinhalten, wie das Projekt auf Git zu organisieren
und dokumentieren ist oder wie beispielsweise Commitnachrichten formuliert
werden sollen.\
Eine gute Kommunikation ist fast das Wichtigste bei größeren Teams. Legt also am
besten direkt einen dedizierten Kommunikationskanal an. Slack oder Discord
bieten sich an. Jeder sollte dort regelmäßig rein schauen oder sogar
Push-Notifications einschalten.\
Stellt immer wieder klar, wer, für was verantwortlich ist. Ansonsten kann es
sein, dass Aufgaben einfach liegen bleiben. Engagement von allen im Team ist
hier für einen erfolgreichen Abschluss der Projektarbeit sehr wichtig.\
Das Projekt findet in einem sehr kurzen Zeitfenster statt. Achtet deshalb auf
eine gute Organisation und Zeitplanung.

## Tipps für Scrum Master und Product Owner

### Product Owner

* Features der Vorgänger erfassen (mithilfe der Teams. Überblick verschaffen.)
* Grobes Ziel für das Ende des Projekts festlegen. Mit allen zusammen. (Bei uns
  war das z. B. ein funktionierendes Billardspiel von Anfang bis Ende, mit
  überwachen von Regeln und Unterstützungen für die Spieler)
* Schon früh und dann regelmäßig realisierbare Sprintgoals für jedes Team
  festlegen. (Nach einer Eingewöhnungsphase erkennen die Teams meist selbst, was
  zu erledigen ist. Dann mit Rücksprache mit den Teams die Goals festlegen.
  Lieber etwas zu hoch gesteckte Goals, da Teams sonst meinen könnten, nichts
  mehr machen zu müssen.)
* Sprintgoals regelmäßig überprüfen. (Können diese erreicht werden und wo gibt
  es Probleme? Proaktiv Probleme lösen.)
* Backlog führen. Dadurch sollte jeder zu jeder Zeit eine Aufgabe haben.
* Die Teams auffordern, Backlog Items aufzusplitten. (Kleinere Probleme kann man
  besser managen und verstehen)
* Viel Rücksprache mit dem Team. Nicht über die Köpfe hinweg entscheiden aber
  auch Führung übernehmen, wenn nötig.

### Scrum Master

* Require weekly reviews from the teams respectively, in order to have
  continuous overview over the current project status. With this, problems can
  be recognized early and countermeasures can be taken.
* Summarize weekly reviews and send to the product owner
* Active communication with the teams during the attendance hours at the
  Hochschule, requesting the team status on site (This procedure is only
  necessary in the first weeks, usually weekly reviews suffice.)
* Prepare sprint planning meeting
* Conduct retrospective with various games in order to assess team satisfaction
  (there are great templates on the internet). In doing so, it is important that
  the retrospective does not take up a lot of time. For each game 5-10 minutes
  max.
* Document retrospective in order to have a comparative value for the next
* sprint meeting
* Schedule a date for integration testing early in the project (important!)

\
**Viel Erfolg! ✌**\
Eure Vorgänger
