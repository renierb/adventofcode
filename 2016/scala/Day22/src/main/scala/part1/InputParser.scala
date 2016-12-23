package part1

import scala.Predef.augmentString
import scala.util.parsing.combinator.JavaTokenParsers

object InputParser extends JavaTokenParsers{

  case class Node(x: Int, y: Int, size: Int, used: Int) {
    def avail: Int = size - used
  }

  def apply(input: String): List[Node] = {
    val result = parseAll(read, input)
    if (result.successful)
      result.get
    else
      throw new Exception("Failed to parse input!")
  }

  def read: Parser[List[Node]] =
    rep("/dev/grid/node-x" ~ wholeNumber ~ "-y" ~ wholeNumber ~ wholeNumber ~ "T" ~ wholeNumber ~ "T" ~ wholeNumber ~ "T" ~ wholeNumber ~ "%" ^^ {
      case _ ~ x ~ _ ~ y ~ size ~ _ ~ used ~ _ ~ _ ~ _ ~ _ ~ _ => Node(x.toInt, y.toInt, size.toInt, used.toInt)
    })
}
