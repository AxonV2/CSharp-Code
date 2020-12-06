package com.example.kotlinapp

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }

    val xd : Double = Double.NaN

    //KOTLIN
    fun Click1(view : View)
    {
        //JAVA
        //Intent i1 = new Intent(this, exe1.class);

        val example = Intent(this, exe1::class.java);


        example.putExtra("extraValue", false)
        startActivity(example)
    }

    fun Click2(view : View)
    {
        startActivity(Intent(this, exe2::class.java))
    }

    fun Click3(view : View)
    {
        startActivity(Intent(this, exe3::class.java))
    }

    fun Click4(view: View) {
        startActivity(Intent(this, Calculator::class.java))
    }
}