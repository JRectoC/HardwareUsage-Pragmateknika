//YWROBOT
//Compatible with the Arduino IDE 1.0
//Library version:1.1
#include <Wire.h>
#include <LiquidCrystal_I2C.h>

LiquidCrystal_I2C lcd(0x27, 20, 2); // set the LCD address to 0x27 for a 16 chars and 2 line display
int count = 0,cLimit =0,secs = 0;
int cpu = 0;
String s = "";
int ss = 0;
String textValue = "";
String gRow1col1 = "";
String gRow1col2 = "";
String gRow2col1 = "";
String gRow2col2 = "";
int idleT = 0;
bool boolIdleT = false;


void setup()
{
  lcd.init();                      // initialize the lcd
  lcd.init();
  // Print a message to the LCD.
  lcd.backlight();
  lcd.setCursor(0, 0);
  lcd.print("Hello, world!");
  delay(1500);
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print("Connection:");
  lcd.setCursor(5, 1);
  lcd.print("Is Ready...");
  Serial.begin(9600);
  Serial.print("Running");
}


void loop()
{

  count +=1;
  //updateDisplay();
  getStringFromSerial();
  delay(1);
  
  

}
void updateDisplay(){
  if(cLimit >= 1000){
    secs +=1;
    cLimit = 0;

    //
    if(idleT >=30000){
        lcd.clear();
        lcd.setCursor(0, 0);
        lcd.print("Connection:");
        lcd.setCursor(5, 1);
        lcd.print("Is Ready...");
        boolIdleT = true;
      }else{
        idleT += 1000;
      }
    
  }else{
    cLimit +=1;
  }
}

void lcdValues(String row1col1,String row1col2,String row2col1,String row2col2){
  //lcd.setCursor(0, 0);
  //lcd.print("                  ");  
  //lcd.setCursor(0, 1);
  //lcd.print("                  "); 
  //row1col1
  if(boolIdleT){
     boolIdleT = false;
     lcd.clear();
  }
  if(!row1col1.equals(gRow1col1)){
    lcd.setCursor(0, 0);
    lcd.print(row1col1);
    gRow1col1=row1col1;
  }
  //row1col2
  if(!row1col2.equals(gRow1col2)){
    lcd.setCursor(8, 0);
    lcd.print(row1col2);
    gRow1col2=row1col2;
  }
  //row2col1
  if(!row2col1.equals(gRow2col1)){  
    lcd.setCursor(0, 1);
    lcd.print(row2col1);
    gRow2col1=row2col1;
  }
  //row2col2
  if(!row2col2.equals(gRow2col2)){
    
    lcd.setCursor(8, 1);
    lcd.print(row2col2);
    gRow2col2=row2col2;
  }

  
  
  
  //lcd.setCursor(7, 0);
  //lcd.print(textValue.length());
  //lcd.setCursor(7, 1);
  //lcd.print(textValue);
}

void disectString(String text){
  String row1col1 = text.substring(0,8);
  String row1col2 = text.substring(8,16);
  String row2col1 = text.substring(16,24);
  String row2col2 = text.substring(24,32);
  lcdValues(row1col1,row1col2,row2col1,row2col2);
}


//Get string from serial
void getStringFromSerial(){
  if(Serial.read()!=-1){
      idleT = 0;
      s = Serial.readString();
      ss = s.length()-1;
      textValue = s.substring(0,ss);
      Serial.print(textValue);
      disectString(textValue);
    }else{
      updateDisplay();
    }
}
