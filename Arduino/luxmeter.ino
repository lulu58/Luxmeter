/*
 * https://elektro.turanis.de/html/prj069/index.html

SDA and SCL on Arduino Nano:
    SDA -> A4
    SCL -> A5
    
Merkmale BH1750
    Lichtintensität: 1 lx bis 65535 lx
    durch spezielle Programmierung von 0,11 lx bis zu 100000 lx
    Betriebsspannung: 3,3V - 6V
    I²C-Schnittstelle mit 2 alternativen Adressen
    Stromverbrauch: 7mA


Helligkeitswerte zum Vergleich
427.000 lx   5 mW Laserpointer, grün (532 nm), 3 mm Strahldurchmesser
105.000 lx  5 mW Laserpointer, rot (635 nm), 3 mm Strahldurchmesser
100.000 lx  Heller Sonnentag
20.000 lx   Bedeckter Sommertag
10.000 lx   Operationssaal
3.500 lx  Bedeckter Wintertag
500 lx    Büro-/Zimmerbeleuchtung
10 lx     Straßenbeleuchtung
1 lx    Kerze (~1m entfernt)
0,25 lx   Vollmondnacht
0,001 lx  Sternklarer Nachthimmel (Neumond)

Verwendete Bibliothek:
http://s6z.de/cms/index.php/arduino/sensoren/15-umgebungslichtsensor-bh1750
https://github.com/hexenmeister/AS_BH1750
-----------------------------------------------------------------------------
*/
#include <Wire.h>
#include <AS_BH1750.h>

AS_BH1750 sensor;

void setup()
{
    Serial.begin(115200);

    // for normal sensor resolution (1 lx resolution, 0-65535 lx, 120ms, no PowerDown)
    // use: sensor.begin(RESOLUTION_NORMAL, false);

    if(!sensor.begin()) {
        Serial.println("BH1750 init failed!");
        while(true);
    }
}

void loop()
{
    float lux = sensor.readLightLevel();
    Serial.println("Light level: " + String(lux) + " lx");
    delay(20);
}
