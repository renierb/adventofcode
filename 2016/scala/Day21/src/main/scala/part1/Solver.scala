package part1

import part1.InputParser.{MoveLetter, ReverseRange, RotateLeft, RotateOnPositionOf, RotateRight, SwapLetter, SwapPosition}

import scala.annotation.tailrec

class Solver(password: String, input: Stream[String]) {

  def solve: String = {
    input.foldLeft(new StringBuilder(password)) {
      case (builder, op) => InputParser(op) match {
        case SwapPosition(x, y) =>
          swapPositions(builder, x, y)
        case SwapLetter(x, y) =>
          swapLetter(builder, x, y)
        case RotateLeft(steps) =>
          rotateLeft(builder, steps)
        case RotateRight(steps) =>
          rotateRight(builder, steps)
        case RotateOnPositionOf(c) =>
          rotateOnPositionOf(builder, c)
        case ReverseRange(x, y) =>
          reverseRange(builder, x, y)
        case MoveLetter(x, y) =>
          moveLetter(builder, x, y)
      }
    }.toString
  }

  protected def swapPositions(builder: StringBuilder, x: Int, y: Int): StringBuilder = {
    val atX = builder(x)
    val atY = builder(y)
    builder(x) = atY
    builder(y) = atX
    builder
  }

  @tailrec
  final protected def swapLetter(builder: StringBuilder, x: Char, y: Char, index: Int = 0): StringBuilder = {
    if (index >= builder.length)
      builder
    else {
      builder(index) match {
        case `x` => builder(index) = y
        case `y` => builder(index) = x
        case _ =>
      }
      swapLetter(builder, x, y, index + 1)
    }
  }

  protected def rotateLeft(builder: StringBuilder, steps: Int): StringBuilder = {
    val length = builder.length
    if (steps % length == 0)
      builder
    else {
      val left = builder.substring(0, steps % length)
      builder.delete(0, steps)
      builder.append(left)
    }
  }

  protected def rotateRight(builder: StringBuilder, steps: Int): StringBuilder = {
    val length = builder.length
    if (steps % length == 0)
      builder
    else {
      val start = length - (steps % length)
      val right = builder.substring(start, length)
      builder.delete(start, length)
      builder.insert(0, right)
    }
  }

  protected def rotateOnPositionOf(builder: StringBuilder, c: Char): StringBuilder = {
    val indexOf = builder.indexWhere(_ == c)
    val amount = indexOf + 1 + (if (indexOf >= 4) 1 else 0)
    rotateRight(builder, amount)
  }

  protected def reverseRange(builder: StringBuilder, x: Int, y: Int): StringBuilder = {
    val letters = builder.substring(x, y + 1)
    builder.replace(x, y + 1, letters.reverse)
  }

  protected def moveLetter(builder: StringBuilder, x: Int, y: Int): StringBuilder = {
    val atX = builder(x)
    builder
      .deleteCharAt(x)
      .insert(y, atX)
  }
}
