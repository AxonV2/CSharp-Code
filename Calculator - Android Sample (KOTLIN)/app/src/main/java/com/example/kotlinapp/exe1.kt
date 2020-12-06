package com.example.kotlinapp

import android.content.Intent
import android.os.Bundle
import android.view.View
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import androidx.core.util.rangeTo
import com.example.kotlinapp.R
import kotlinx.android.synthetic.main.activity_exe1.*
import kotlinx.android.synthetic.main.activity_exe4.*

class exe1 : AppCompatActivity() {


    private var stri = true;


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_exe1)

                        //Capitalization Matters
       stri = intent.getBooleanExtra("extraValue",true)

    }

    fun clickAlterar(view: View)
    {
        textView2.text = "Nome Alterado!!!"
        textView2.text = stri.toString();


        for (i in 4 downTo 1)
            textView2.text = "${textView2.text}+${i}"

        for(i in 1 .. 20)
            textView2.text = "${textView2.text}+${i}"

        for(i in 1 .. 20 step 1)
            textView2.text = "${textView2.text}+${i}"

        var numb = 200;

        do {
            textView2.text = "${textView2.text}+${numb}"
            numb /= 10
        }while (numb > 0)

        val str = "lmao"
        val arr = mutableListOf<Double>(1.0,2.0,3.0)

        for (i in arr.indices)
            str[i] //INDICES GIVES INDEX VALUE

        for(i in arr)
            i //THIS GIVES ACTUAL VALUE


        val x : Int = 13;
        when(x){
            in 1..5 -> textView2.text = "1 - 5"
            in 10 downTo 5 -> textView2.text = "10 - 5"
            in 11 rangeTo  15 -> textView2.text = "10 up"
            in 20 until 30 -> textView2.text = "test"
            16,17,18,19,20 -> textView2.text = "Up 15"
            //is String -> x.contains() WOULD WORK HAVE TO SET x : Any
        }

        val y = "Always";

        //WHEN ACTING AS AN IF STATEMENT INSTEAD
        //NO ARGUMENTS WERE PASSED IN
        when{
            y.contains("a") -> textView2.text = "1 - 5"
            y.length > 5 -> textView2.text = "10 - 5"
        }

    }
}