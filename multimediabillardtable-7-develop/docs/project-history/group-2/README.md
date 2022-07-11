Multimedia Billiard Table
=========================


Licence
-------
It is intended to publish this repository as an Open Source Project. The exact licence still needs to be decided. For now, it is not allowed to distribute code or design material outside of h-da. As a student, you can download to your own PC for the purpose of contributing to the project during the course. 

- Only add code written by yourself. Do not copy & paste random code snippets 'found somewhere on the web'.

- Do not add material with unclear license or with rights held by third persons.

- When the project will be published, you will be asked to either 

  - be named as one of the authors

  - stay anonymous instead.

Further details will be discussed in the course.
 

Hardware Concept
----------------

* Frame made from aluminum profile used in industrial automation. Grooves can be used to fix electronic equipment, wires can be run inside the grooves. Resembles our recommended office furniture color scheme (maple/silver) and is sturdy. 

* Billiard tables are 8 foot (junior and women championships) or 9 foot (men championships etc). Internal championships and pubs often host 7 foot tables. For home use also smaller sizes exist, but real billiard starts with 8 foot tables. Our table is 8 foot, as an institution of education, we should have large enough rooms.

* The table should be multi use, as we can not block a room forever. First considerations for a fold-up-and-roll-away design have been dropped for reasons of safety and complexity. Instead, we follow the mindset of the "fusion table" from Belgium (https://www.fusiontables.com). Here, the billiard table is normally not visible, it's appearance is that of an ordinary dining table. In our context, we want to convert a meeting table into a billiard table -- just take away the tabletop and let it lift to billiard height.

* Rubber cushions and pockets are easily available. We aim at highly regarded standard material: our table should not be compromised in the game play. 
 * For rubber cushions profile K66 from a German manufacturer is highly reputated (https://www.artemis-kautschuk.de/de/billardbanden.php).
 * For the cloth we use Iwan Simmonis 760 (certified for tournaments, somewhat faster than 860, but due to its smaller thickness we hope it to be usable for rear projection).
 * We use artemide Premium balls (certified for tournaments, there is one superior category still).
 * We will go with standard cues for a start. 

* The best possible choice of cloth color is gray. The leading manufacturer, Ivan Simonis in Belgium, claims that their cloth is dyed in the factory, so in theory one could receive undyed cloth from there. An email request if this could be possible remained unanswered. First projection trials suggest that even gray is too dark, and we may have to resort to rear projection cloth, or project from above.

* Plexiglas GS 0F00 in 25mm thickness is 275,47 €/m², so the cost of a transparent 'slate' is about 860.-. This is considerably more than a real slate (about 420.- for 8 foot). 25mm is the maximum thickness in the standard range (that has various colors available). There are also 'blocks' available in 30,35,40,...80 (only clear color which is what we need). Obviously the board has to be stable enough that it doesn't sag or break when a player leans on. Prof. Hergenröther will clarify appropriate thickness with Department of Mechanical and Plastics Engineering.

* Sizes are derived from a billiard slate for which a technical drawing was found in the net. An exchange later on with a real slate should therefore be easily possible if rear projection fails.

* For the projector we are still considering options, but target for 'ultra short throw' projectors. 

* Cameras will be integrated into the 6 pockets, further sensing tbd.


BOM
---

|Description | approx. Cost | Source |
|---|---:|---|
| **Construction** 			|**2400.00**| |
| Aluminum profiles and fasteners	|1265.00 | https://www.item24.de |
| Transparent 'slate' PMMA 25mm		| 870.00 | https://www.plexiglas-shop.com |
| Wood (estimation)			| 100.00 | h_da Schreinerei Dieburg |
| Metal sheet, Aluminium blocks	(est.)	| 100.00 | h_da Zentralwerkstatt Darmstadt |
| Small parts (est.)			|  65.00 | |
| **Billiard specifics**		|**699.00**| |
| Simmonis 760 grey 3m			| 190.00 | http://www.veith-group.de/shop/contents/de/d143_Simonis_760.html |
| Rubber cushion Artemis Pool 66	| 190.00 | http://www.veith-group.de/shop/contents/de/d164_Bandenprofile.html |
| Ball set pool Aramith Premium		| 110.00 | https://www.dynamic-billard.de/de/billard-zubehoer/billardkugeln/billardkugeln-fuer-pool/1295/billardkugelset-pool-aramith-premium-57-2-mm?c=42100 |
| Rack					|   7.00 | https://www.dynamic-billard.de/de/billard-zubehoer/billardtisch-zubehoer/billard-dreiecke/1221/dreieck-pool-wm-special-schwarz-57-2-mm |
| 4 Cues				|  66.00 | https://www.dynamic-billard.de/de/billardqueues/pool-queues/einteilige-queues-fuer-pool-billard/186/billardqueue-pool-jupiter-ju-2-schwarz-einteilig?c=200 |
| Facings 5mm				|   5.00 | https://www.dynamic-billard.de/de/billard-zubehoer/billardtisch-zubehoer/ersatzteile-und-werkzeug-fuer-billardtische/1365/banden-facings-pool-5-mm-12-stueck |
| 6 Cloth fasteners			|  16.00 | https://www.dynamic-billard.de/de/billard-zubehoer/billardtisch-zubehoer/ersatzteile-und-werkzeug-fuer-billardtische/1217/tuch-klemmleiste-fuer-dynamic-tische?number=70.002.00.0 |
| Ball return system			| 115.00 | https://www.dynamic-billard.de/de/billard-zubehoer/billardtisch-zubehoer/ersatzteile-und-werkzeug-fuer-billardtische/790/ballruecklaufsystem-fuer-eliminator/triumph-7-8-ft.-fuss?c=43400 |
| **RGB-LED**				| **90.00**| |
| RGB LED stripe WS2812B 60/m 7m	|  62.00 | https://www.sertronics-shop.de/bauelemente/aktive-bauelemente/leds/digitale-leds-module-stripes/3422/digitaler-led-rgb-neopixel-stripe-ip65-vergossen-60-leds/m-schwarze-leiterbahn-meterware |
| 18 RGB LEDs WS2812B SMD		|   6.00 | https://www.sertronics-shop.de/bauelemente/aktive-bauelemente/leds/digitale-leds-module-stripes/2283/worldsemi-digitale-ws2812b-5050-smd-led-weiss |
| Powersupply 5V 18A			|  22.00 | https://www.sertronics-shop.de/raspberry-pi-co/raspberry-pi/stromversorgung/einbau-industrienetzteilmodule/2879/meanwell-1-kanal-einbau-schaltnetzteil-90w-5v/18a |
| **Automatic Leveling** 		| **97.00**| |
| 4 geared motors 12V			|  60.00 | https://www.pollin.de/p/gleichstrom-getriebemotor-gmpd-404980-1-12-v-310477 |
| Powersupply 12V 8.5A			|  17.00 | https://www.pollin.de/p/schaltnetzteil-meanwell-lrs-100-12-12-v-8-5-a-352181 |
| Sensing (est.)			|  20.00 | tbd. |
| **Ball sensing**			|**250.00**| |
| 6 Raspberry Pi Zero W (+header)	|  76.00 | https://www.raspberrypi.org/products/raspberry-pi-zero-w/ |
| 6 Pi Camera V2			| 138.00 | https://www.raspberrypi.org/products/camera-module-v2/ |
| 6 original Raspberry Case for Zero	|  36.00 | https://www.raspberrypi.org/products/raspberry-pi-zero-case/ |
| **Projection**			|**1300.00-4500.00** | |
| Dell S718QL (4k, Laser, 5000lm)	|4500.00 | https://www.dell.com/de-de/shop/dell-advanced-4k-laserprojektor-s718ql/apd/210-amod/projektoren-und-projektorzubehör |
| or InFocus INL148HDUST (FullHD, Laser 4000lm)|2520.00 | https://www.infocus.com/products/inl148hdust |
| or Optoma HD35UST (FullHD, 3600lm)	|1600.00 | https://www.optoma.de/product-details/hd35ust |
| or Optoma HD31UST (FullHD, 3400lm)	|1300.00 | https://www.optoma.de/product-details/hd31ust | 
| PC (est.)				|(600.00)| (use existing) |

| **Total (w/o PC)**			|**4836.00-8036.00** |
|---|---:|
| with Dell S718QL (4k, Laser, 5000lm)	|8036.00 | 
| with InFocus INL148HDUST (FullHD, Laser 4000lm)|6056.00 |
| with Optoma HD35UST (FullHD, 3600lm)	|5136.00 |
| with Optoma HD31UST (FullHD, 3400lm)	|4836.00 |
| with 2 Optoma HD31UST (FullHD, 3400lm)|6136.00 |


Software Architecture Concept
-----------------------------

* We start with simple use cases and gradually improve from there. Color change from player to player, light (and sound) effects following balls and shots. Next support for rail shots (https://www.youtube.com/watch?v=KJ28PkeLmds). Support with english (side spin), follow and draw ball (https://www.youtube.com/watch?v=vKD7EdzMjAM). Finally artistic pool, with figures projected from below http://www.artisticpoolplayers.com/shot_programs/ArtisticPool2012.pdf and trick shots (https://www.youtube.com/watch?v=1zu_fEWfxFQ)

* A distributed system consisting of components communicating via MQTT http://mqtt.org/. MQTT is rather easy to implement, missing components can be simulated, and software components are only loosely coupled and can thus be implemented by different teams more easily. Mosquitto is a broker that is available under several OS, and libmosquitto (libmosquittopp for C++) is the library we will base our implementation upon.

* In each pocket is a Raspberry Pi Zero W with camera, 6 in total, overlooking the situation in front of the pockets and registering which ball has dropped inside. Images from the camera can be directly processed with an OpenGL shader without extra memory transfer. It should be feasible to include an RGB to HSV converter as well as color classifier in the shader code in order to do most processing parallelized inside the graphics hardware. The raspberry camera is well known and broadly used, and there is example code available of how to process camera pictures with shaders https://github.com/raspberrypi/userland/blob/master/host_applications/linux/apps/raspicam/gl_scenes/sobel.c

* The diamonds (or sights) on the rails are required as they are referenced in the rules. We will illuminate them with RGB LEDs. They will be controlled by one of the Raspberry Zero computers, a software component will light them according to MQTT messages that it subscribes.

* For a more precise indication of a rail position and to enrich the output possibilities we include RGB stripes with individually addressable LEDs below the cloth that covers the rails, just behind the rubber cushion. 

* Height needs to be adjusted from 72-76 cm when used as a meeting table to 74,3-78,7 cm (not including rails and tabletop) for playing billiard. We plan another embedded system (or one of the Zeros) to level the table. Geared motors are integrated inside the frame that rotate the threaded knuckles in the table legs to change the height. MEMS accelerometers or another sensing is used to level the table, which is not an easy task with four legs. 

* The visualizations and supported game play still need to be planned in detail. Examples are
  * Aura around balls (flames, whirls, particles,...)
  * supporting or irritating patterns (as a handicap)
  * predicting the future based on cue direction and ball positions
  * telebilliard (courtesy Steffen Czerwinski) where a distant player has to lay out balls according to the projected positions of the other player prior to making a shot.

* There may be multiple different applications that make use of the billiard table sensing and LED control via subscribed and published MQTT messages

* MQTT message definition shall follow a clear ontology, as components will be reused in following terms.


