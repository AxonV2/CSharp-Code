package com.example.kotlinapp

import android.os.Bundle
import android.os.Handler
import android.view.View
import android.widget.Button
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import kotlinx.android.synthetic.main.activity_exe4.*
import kotlin.math.round

class Calculator : AppCompatActivity()
{
    private lateinit var view : TextView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_exe4)

        view = findViewById(R.id.NumberView)


    }

    private var Auto_Inference = Double.NaN;
    private var First_Number: Double = Double.NaN;
    private var Second_Number: Double = Double.NaN;
    private var Operator: String = "";

    private fun View_Clear(view: View)
    {
        NumberView.text = ""
        ResultView.text = ""
    }

    fun Full_Clear(view: View)
    {
        View_Clear(view)
        First_Number = Double.NaN
        Second_Number = Double.NaN
        Operator = ""
    }

    fun Back_Button(view: View)
    {
        //Remove single number at end
        if (!NumberView.text.isNullOrEmpty())
            NumberView.text = NumberView.text.subSequence(0, NumberView.length() - 1);
    }

    //region Numbers
    fun Zero(view: View) { NumberView.text = "${NumberView.text}0" }
    fun One(view: View) { NumberView.text = "${NumberView.text}1" }
    fun Two(view: View) { NumberView.text = "${NumberView.text}2" }
    fun Three(view: View) { NumberView.text = "${NumberView.text}3" }
    fun Four(view: View) { NumberView.text = "${NumberView.text}4" }
    fun Five(view: View) { NumberView.text = "${NumberView.text}5" }
    fun Six(view: View) { NumberView.text = "${NumberView.text}6" }
    fun Seven(view: View) { NumberView.text = "${NumberView.text}7" }
    fun Eight(view: View) { NumberView.text = "${NumberView.text}8" }
    fun Nine(view: View) { NumberView.text = "${NumberView.text}9" }

    fun Dot(view: View)
    {
        if (NumberView.text.isNullOrEmpty())
            NumberView.text="${NumberView.text}0."

        if (!NumberView.text.contains('.'))
            NumberView.text = "${NumberView.text}."
    }
    //endregion

    //region Operators

    fun Div(view: View) { Operation_Set(view, "/") }

    fun Mult(view: View) { Operation_Set(view, "*") }

    fun Minus(view: View) { Operation_Set(view, "-") }

    fun Plus(view: View) { Operation_Set(view, "+") }


    private fun Operation_Set(view: View, operationVal: String)
    {
        Operator = operationVal;
        First_Number_Set(view)
    }

    //endregion

    private fun First_Number_Set(view: View)
    {
        if (ResultView.text.isNotEmpty() && NumberView.text.isEmpty())
        {
            //Dogshit
            //if (!check(ResultView,'-') && !check(ResultView,'+') && !check(ResultView,'*') && !check(ResultView,'/'))

            //Simplified
            if (!Double_Operator_Check(ResultView))
                First_Number = ResultView.text.toString().toDouble();

            ResultView.text = "$First_Number $Operator";
        }
        else if (!NumberView.text.isNullOrEmpty())
        {
            First_Number = NumberView.text.toString().toDouble();
            NumberView.text = ""
            ResultView.text = "$First_Number $Operator";
        }
    }

    private fun Double_Operator_Check(View_Param : TextView) : Boolean
    {
        val CharacterSequence : CharSequence = "+-*/"

        //for (numb in 0 until CharacterSequence.length)
        // if (View_Param.text.contains(CharacterSequence[numb]))

        for (element in CharacterSequence)
            if (View_Param.text.contains(element))
                return true;

        return false;
    }
    
    fun Equals(view: View)
    {
        if (Operator.isNotEmpty() && !First_Number.isNaN() && NumberView.text.isNotEmpty())
        {
            Second_Number = NumberView.text.toString().toDouble()


            View_Clear(view)

            when(Operator)
            {
                "-" -> ResultView.text = round((First_Number - Second_Number)).toString()
                "*" -> ResultView.text = round((First_Number * Second_Number)).toString()
                "/"-> ResultView.text = round((First_Number / Second_Number)).toString()
                "+" -> ResultView.text = round((First_Number + Second_Number)).toString()
            }

            Operator=""
        }
    }


}