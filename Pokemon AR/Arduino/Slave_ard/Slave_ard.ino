#include <Wire.h>
#include <FastLED.h>

#if defined(FASTLED_VERSION) && (FASTLED_VERSION < 3001000)
#warning "Requires FastLED 3.1 or later; check github for latest code."
#endif

#define DATA_PIN    3
//#define CLK_PIN   4
#define LED_TYPE    WS2813
#define COLOR_ORDER GRB
#define NUM_LEDS    30
CRGB leds[NUM_LEDS];

#define BRIGHTNESS          96
#define FRAMES_PER_SECOND  120

int red_light_1 = 10;
int green_light_1 = 11;
int blue_light_1 = 12;

int red_light_2 = 6;
int green_light_2 = 7;
int blue_light_2 = 8;

int damageTaken = 0;
int damageGiven = 0;
int totalDamage = 0;


void setup() {
  Serial.begin(9600);

  FastLED.addLeds<LED_TYPE,DATA_PIN,COLOR_ORDER>(leds, NUM_LEDS).setCorrection(TypicalLEDStrip);

  FastLED.setBrightness(BRIGHTNESS);

  pinMode(red_light_1, OUTPUT);
  pinMode(green_light_1, OUTPUT);
  pinMode(blue_light_1, OUTPUT);
  pinMode(red_light_2, OUTPUT);
  pinMode(green_light_2, OUTPUT);
  pinMode(blue_light_2, OUTPUT); 

  for (int i = 0; i < NUM_LEDS; i++){
      leds[i] = CRGB::Green;
      
  delay(100);
  FastLED.show();
  }

  Wire.begin(9);
  Wire.onReceive(receiveEvent);
}

void loop() {
  // put your main code here, to run repeatedly:

}

void RGB_color(int red_light_1v, int green_light_1v, int blue_light_1v, int red_light_2v, int green_light_2v, int blue_light_2v){
  analogWrite(red_light_1, red_light_1v);
  analogWrite(green_light_1, green_light_1v);
  analogWrite(blue_light_1, blue_light_1v);
  analogWrite(red_light_2, red_light_2v);
  analogWrite(green_light_2, green_light_2v);
  analogWrite(blue_light_2, blue_light_2v);
}

void receiveEvent (int bytes){
  damageGiven = Wire.read();
  damageTaken += damageGiven;
  totalDamage = NUM_LEDS - damageTaken;
  takeDamage();
}

void takeDamage(){
  
      for (int k = 0; k < NUM_LEDS; k++){
      leds[k] = CRGB::Black;
      FastLED.show();
    }
    RGB_color(255,0,0,255,0,0);
    delay(100);
        for (int l = 0; l < NUM_LEDS; l++){
      leds[l] = CRGB::Green;
      FastLED.show();
    }
    RGB_color(0,0,0,0,0,0);
    delay(100);
    for (int m = 0; m < NUM_LEDS; m++){
      leds[m] = CRGB::Black;
      FastLED.show();
    }
    RGB_color(255,0,0,255,0,0);
    delay(100);
        for (int n = 0; n < NUM_LEDS; n++){
      leds[n] = CRGB::Green;
      FastLED.show();
    }
    RGB_color(0,0,0,0,0,0);
    delay(100);
        for (int o = 0; o < NUM_LEDS; o++){
      leds[o] = CRGB::Black;
      FastLED.show();
    }
    RGB_color(255,0,0,255,0,0);
    delay(100);
        for (int p = 0; p < NUM_LEDS; p++){
      leds[p] = CRGB::Green;
      FastLED.show();
    }
    RGB_color(0,0,0,0,0,0);
    delay(100);

for (int j = NUM_LEDS; j >= totalDamage; j--){
      leds[j] = CRGB::Red;

  delay(100);
  FastLED.show();
    }
}

void resetHealth(){
  damageTaken = 0;
  totalDamage = 0;
  
  for (int i = 0; i < NUM_LEDS; i++){
      leds[i] = CRGB::Green;
      
  delay(100);
  FastLED.show();
  }
}
