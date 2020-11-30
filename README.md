# Assignment_6  

![build pass](https://img.shields.io/badge/build-pass-brightgreen) ![platform unity](https://img.shields.io/badge/platform-unity-red)  
This game is an implementation of the brick breaker game as part of Game Development course.
This project shows how to create a difficulty levels. Each level changes the following attributes:  
- Player speed  
- Amount of bricks
- Toughness of bricks

## Level 1

![ללא שם](https://user-images.githubusercontent.com/57867818/100528277-acb7dc80-31e3-11eb-897a-830b9d452762.png)  

This is a standard brick breaker game , reasonable amount of bricks and relativly fast.

## Level 2

![ללא שם](https://user-images.githubusercontent.com/44766214/100671621-77d59200-3369-11eb-99b1-ff6b7c7dd2c5.png)  

The second stage contain **hard** bricks. This means the player has to hit them twice in order to break them.
Also, the bricks located high which add difficulty to the game. 
The player will be a bit slower than the first level. This will makes the stage harder as well.

## Level 3
![ללא שם](https://user-images.githubusercontent.com/44766214/100671776-b5d2b600-3369-11eb-8f74-1dc46f2f400a.png)  

The third stage contain a lot of bricks, more than the amount of bricks both in levels one and two.  
In addition the bricks arranged in a complex settings in order to make the stage hard to pass.  
Besides that, the bricks will be located closer to the player which makes the whole level, and especially the start, harder.  
In addition, the player's speed will be reduced.

## level 4

![ללא שם](https://user-images.githubusercontent.com/44766214/100671954-ec103580-3369-11eb-8b24-34ba7e3b5d4b.png)  

The fourth and last stage is the hardest. Player will be extreme slow, bricks will be set in an inconvenience way.  
The bricks in the stage will be both normal and hard bricks. 
In addition there is a **boss** brick located in the middle of the stage.  
It is a super size brick which will be broken only after 20 hits.

## Design and Code
The mains scripts are for the player and for the ball. we added colliders and rigid body for both of them.
### Ball's movment
When the ball hits the player, we are calculating the new movement vector of the ball according to the absolute delta between the ball.transform.x and the player.transform.x. We then use this delta as negative x in the following manner:  
```
float delta_x = col.gameObject.GetComponent<Transform>().position.x - GetComponent<Transform>().position.x;
GetComponent<Rigidbody2D>().velocity = new Vector2(-delta_x, 1).normalized * PlayerPrefs.GetInt("speed");
```
### Levels
The transition between each level is made by an array of brick's level which holds the amount of bricks the player has to destroy in order to finish the level.
This mechanism helps us integrate new level's easily, all you have to do is to had the new number as const and put it in the array:
```
void next_level()
{
  PlayerPrefs.SetInt("bricks", amount_of_bricks_level[PlayerPrefs.GetInt("level") - 1]);
  PlayerPrefs.SetInt("count", 0);//Set current amount of brick to zero (at the begining of new level)
  PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex + 1);
  PlayerPrefs.Save();

  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
}
```
