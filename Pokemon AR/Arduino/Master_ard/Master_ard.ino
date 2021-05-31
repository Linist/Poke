#include <Wire.h>
#include <FastLED.h>

//checks the version of FastLED
#if defined(FASTLED_VERSION) && (FASTLED_VERSION < 3001000)
#warning "Requires FastLED 3.1 or later; check github for latest code."
#endif

//sets up FastLED strip
#define DATA_PIN    3
//#define CLK_PIN   4
#define LED_TYPE    WS2813
#define COLOR_ORDER GRB
#define NUM_LEDS    30
CRGB leds[NUM_LEDS];

#define BRIGHTNESS          96
#define FRAMES_PER_SECOND  120

// variables for the LED cubes pins
int red_light_1 = 10;
int green_light_1 = 11;
int blue_light_1 = 12;

int red_light_2 = 6;
int green_light_2 = 7;
int blue_light_2 = 8;

//variables for damage
int newDamage = 0;
int damageTaken = 0;
int totalDamage = 0;
int damageLED = 0;

//variables that can be send to the other arduino
int damageSend = 0;
int resetHP = 40;
int battleVar = 999;




void setup() {
  //sets up the serial port.
  Serial.begin(9600);

  //setup for the LED strip
  FastLED.addLeds<LED_TYPE,DATA_PIN,COLOR_ORDER>(leds, NUM_LEDS).setCorrection(TypicalLEDStrip);

  //sets the brightness of the LED strip (0 - 255)
  FastLED.setBrightness(BRIGHTNESS);

  //setup the pinmodes for the LED cubes.
  pinMode(red_light_1, OUTPUT);
  pinMode(green_light_1, OUTPUT);
  pinMode(blue_light_1, OUTPUT);
  pinMode(red_light_2, OUTPUT);
  pinMode(green_light_2, OUTPUT);
  pinMode(blue_light_2, OUTPUT);  

  //fills the LED strip with green, by a trail pattern.
  for (int i = 0; i < NUM_LEDS; i++){
      leds[i] = CRGB::Green;
  //this delay makes the light not appear instantly, but get the trail pattern    
  delay(100);
  FastLED.show();
  }
  //starts the I2C connection
  Wire.begin();
  //sets the two LED cubes to green
  RGB_color(0,255,0,0,255,0);
}


void loop() {
  // a few cases
  switch (Serial.read()){
  case 'a':
    damageSend = 1;
    giveDamage();
    break;

  case 'b':
    damageSend = 2;
    giveDamage();
    break;

  case 'c':
    damageSend = 3;
    giveDamage();
    break;

  case 'd':
    damageSend = 4;
    giveDamage();
    break;

  case 'e':
    damageSend = 5;
    giveDamage();
    break;

   case 'f':
    damageSend = 6;
    giveDamage();
    break;

  case 'g':
    damageSend = 7;
    giveDamage();
    break;

  case 'h':
    damageSend = 8;
    giveDamage();
    break;

  case 'i':
    damageSend = 9;
    giveDamage();
    break;

  case 'j':
    damageSend = 10;
    giveDamage();
    break;

  case 'k':
    newDamage = 11;
    giveDamage();
    break;

  case 'l':
    damageSend = 12;
    giveDamage();
    break;

  case 'm':
    damageSend = 13;
    giveDamage();
    break;

  case 'n':
    damageSend = 14;
    giveDamage();
    break;

  case 'o':
    damageSend = 15;
    giveDamage();
    break;

  case 'p':
    damageSend = 16;
    giveDamage();
    break;

  case 'q':
    damageSend = 17;
    giveDamage();
    break;

  case 'r':
    damageSend = 18;
    giveDamage();
    break;

  case 's':
    damageSend = 19;
    giveDamage();
    break;

  case 't':
    damageSend = 20;
    giveDamage();
    break;

  case 'u':
    damageSend = 21;
    giveDamage();
    break;

  case 'v':
    damageSend = 22;
    giveDamage();
    break;

  case 'w':
    damageSend = 23;
    giveDamage();
    break;

  case 'x':
    damageSend = 24;
    giveDamage();
    break;

  case 'y':
    damageSend = 25;
    giveDamage();
    break;

  case 'z':
    damageSend = 26;
    giveDamage();
    break;

  case '0':
    damageSend = 27;
    giveDamage();
    break;

  case '9':
    damageSend = 28;
    giveDamage();
    break;

  case '8':
    damageSend = 29;
    giveDamage();
    break;

  case '7':
    damageSend = 30;
    giveDamage();
    break;
    
  case 'A':
    newDamage = 1;
    calculateDamage();
    break;
    
  case 'B':
    newDamage = 2;
    calculateDamage();
    break;
    
  case 'C':
    newDamage = 3;
    calculateDamage();
    break;
    
  case 'D':
    newDamage = 4;
    calculateDamage();
    break;
    
  case 'E':
    newDamage = 5;
    calculateDamage();
    break;
    
  case 'F':
    newDamage = 6;
    calculateDamage();
    break;
    
  case 'G':
    newDamage = 7;
    calculateDamage();
    break;
    
  case 'H':
    newDamage = 8;
    calculateDamage();
    break;
    
  case 'I':
    newDamage = 9;
    calculateDamage();
    break;
    
  case 'J':
    newDamage = 10;
    calculateDamage();
    break;
    
  case 'K':
    newDamage = 11;
    calculateDamage();
    break;
    
  case 'L':
    newDamage = 12;
    calculateDamage();
    break;
    
  case 'M':
    newDamage = 13;
    calculateDamage();
    break;
    
  case 'N':
    newDamage = 14;
    calculateDamage();
    break;
    
  case 'O':
    newDamage = 15;
    calculateDamage();
    break;
    
  case 'P':
    newDamage = 16;
    calculateDamage();
    break;
    
  case 'Q':
    newDamage = 17;
    calculateDamage();
    break;
    
  case 'R':
    newDamage = 18;
    calculateDamage();
    break;
    
  case 'S':
    newDamage = 19;
    calculateDamage();
    break;
    
  case 'T':
    newDamage = 20;
    calculateDamage();
    break;
    
  case 'U':
    newDamage = 21;
    calculateDamage();
    break;
    
  case 'V':
    newDamage = 22;
    calculateDamage();
    break;
    
  case 'W':
    newDamage = 23;
    calculateDamage();
    break;
    
  case 'X':
    newDamage = 24;
    calculateDamage();
    break;
    
  case 'Y':
    newDamage = 25;
    calculateDamage();
    break;
    
  case 'Z':
    newDamage = 26;
    calculateDamage();
    break;
    
  case '1':
    newDamage = 27;
    calculateDamage();
    break;
    
  case '2':
    newDamage = 28;
    calculateDamage();
    break;
    
  case '3':
    newDamage = 29;
    calculateDamage();
    break;
    
  case '4':
    newDamage = 30;
    calculateDamage();
    break;

  case '5':
    resetHealth();
    slaveHeal();
    break;
  }

}

//Picking color for the two LED cubes
void RGB_color(int red_light_1v, int green_light_1v, int blue_light_1v, int red_light_2v, int green_light_2v, int blue_light_2v){
  analogWrite(red_light_1, red_light_1v);
  analogWrite(green_light_1, green_light_1v);
  analogWrite(blue_light_1, blue_light_1v);
  analogWrite(red_light_2, red_light_2v);
  analogWrite(green_light_2, green_light_2v);
  analogWrite(blue_light_2, blue_light_2v);
}


// sends the damage to the other arduino
void giveDamage(){
  Wire.beginTransmission(9);
  Wire.write(damageSend);

  Wire.endTransmission();
  
  
}

void slaveHeal(){
  Wire.beginTransmission(9);
  Wire.write(resetHP);
  Wire.endTransmission();
}


void calculateDamage(){
  //takes the damage from the case and adds it to variable
  damageTaken += newDamage;
  //calculate the total damage the pokemon has taken.
  totalDamage = NUM_LEDS - damageTaken;
  //sets the total damage to a new variable, since you can't change a variable being used in a FastLED loop directly.
  damageLED = totalDamage;
  //starts the damage process
  takeSomeDamage();
}

//sets all the variables to 0, so we can play a new battle.
void noDamage(){
  totalDamage = 0;
  damageTaken = 0; 
}

// general function for taking damage, makes the LEDs flash and then turns the health lost to red LEDs
void takeSomeDamage(){

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

    RGB_color(255,0,0,255,0,0);
  
  for (int j = NUM_LEDS; j >= damageLED; j--){
      leds[j] = CRGB::Red;

  delay(100);
  FastLED.show();
    }  
  
}

//resets the health for the pokemon
void resetHealth(){ 


  for (int i = 0; i < NUM_LEDS; i++){
      leds[i] = CRGB::Green;
      
  delay(100);
  FastLED.show();
  }
  RGB_color(0,255,0,0,255,0);
  noDamage();
}
