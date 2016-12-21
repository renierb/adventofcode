package part1

import scala.util.parsing.combinator.JavaTokenParsers

object InputParser extends JavaTokenParsers{

  case class Disc(nr: Int, positions: Int, position: Int)

  def apply(input: String): List[Disc] = {
    val result = parseAll(decode, input)
    if (result.successful)
      result.get
    else
      throw new Exception("Parsing input failed!")
  }

  def decode: Parser[List[Disc]] =
    rep("^Disc #".r ~ wholeNumber ~ "has" ~ wholeNumber ~ "positions; at time=" ~ wholeNumber ~ ", it is at position" ~ wholeNumber ~ "." ^^ {
      case _ ~ discNr ~ _ ~ positions ~ _ ~ _ ~ _ ~ position ~ _ => Disc(discNr.toInt, positions.toInt, position.toInt)
    })
}
