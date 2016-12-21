package part2

import part1.InputParser
import part1.InputParser._

class Solver(password: String, input: Stream[String]) extends part1.Solver(password, input) {

  override def solve: String = {
    input.foldLeft(new StringBuilder(password)) {
      case (builder, op) => InputParser(op) match {
        case SwapPosition(x, y) =>
          swapPositions(builder, x, y)
        case SwapLetter(x, y) =>
          swapLetter(builder, x, y)
        case RotateLeft(steps) =>
          rotateRight(builder, steps) // un-scramble by rotating in opposite direction
        case RotateRight(steps) =>
          rotateLeft(builder, steps) // un-scramble by rotating in opposite direction
        case RotateOnPositionOf(c) =>
          rotateOnPositionOf(builder, c)
        case ReverseRange(x, y) =>
          reverseRange(builder, x, y)
        case MoveLetter(x, y) =>
          moveLetter(builder, y, x) // un-scramble by swapping x and y
      }
    }.toString
  }

  override def rotateOnPositionOf(builder: StringBuilder, c: Char): StringBuilder = {
    val length = builder.length
    val indexOf = builder.indexWhere(_ == c)
    val original = reverseLookup(length)(indexOf)
    if (original > indexOf) {
      rotateRight(builder, original - indexOf)
    } else {
      rotateLeft(builder, indexOf - original)
    }
  }

  def reverseLookup(length: Int): Map[Int, Int] = {
    (0 until length).map { x =>
      val y = (x + (x + 1) + (if (x >= 4) 1 else 0)) % length
      (y, x)
    }.toMap
  }
}
