package part1

import scala.util.parsing.combinator.JavaTokenParsers

object InputParser extends JavaTokenParsers {

  def apply(input: String): Op = {
    val parsed = parseAll(decode, input)
    if (parsed.successful)
      parsed.get
    else
      throw new Exception("Failed to parse an operation!")
  }

  sealed trait Op
  case class SwapPosition(x: Int, y:Int) extends Op
  case class SwapLetter(x: Char, y: Char) extends Op
  case class RotateLeft(steps: Int) extends Op
  case class RotateRight(steps: Int) extends Op
  case class RotateOnPositionOf(letter: Char) extends Op
  case class ReverseRange(x: Int, y: Int) extends Op
  case class MoveLetter(x: Int, y: Int) extends Op

  private def decode: Parser[Op] =
    swapPosition | swapLetter | rotateLeft | rotateRight | rotateOnPositionOf | reverseRange | moveLetter

  private def swapPosition: Parser[SwapPosition] =
    "^swap position".r ~ wholeNumber ~ "with position" ~ wholeNumber ^^ {
      case _ ~ x ~ _ ~ y => SwapPosition(x.toInt, y.toInt)
    }

  private def swapLetter: Parser[SwapLetter] =
    "^swap letter".r ~ ident ~ "with letter" ~ ident ^^ {
      case _ ~ x ~ _ ~ y => SwapLetter(x.charAt(0), y.charAt(0))
    }

  private def rotateLeft: Parser[RotateLeft] =
    "^rotate left".r ~ wholeNumber ~ "step(s)?".r ^^ {
      case _ ~ steps ~ _  => RotateLeft(steps.toInt)
    }

  private def rotateRight: Parser[RotateRight] =
    "^rotate right".r ~ wholeNumber ~ "step(s)?".r ^^ {
      case _ ~ steps ~ _  => RotateRight(steps.toInt)
    }

  private def rotateOnPositionOf: Parser[RotateOnPositionOf] =
    "^rotate based on position of letter".r ~ ident ^^ {
      case _ ~ letter  => RotateOnPositionOf(letter.charAt(0))
    }

  private def reverseRange: Parser[ReverseRange] =
    "^reverse positions".r ~ wholeNumber ~ "through" ~ wholeNumber ^^ {
      case _ ~ x ~ _ ~ y  => ReverseRange(x.toInt, y.toInt)
    }

  private def moveLetter: Parser[MoveLetter] =
    "^move position".r ~ wholeNumber ~ "to position" ~ wholeNumber ^^ {
      case _ ~ x ~ _ ~ y  => MoveLetter(x.toInt, y.toInt)
    }
}
