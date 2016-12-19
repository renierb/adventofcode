package part1

import scala.util.parsing.combinator.JavaTokenParsers

sealed trait Item {
  val isotope: Char
}
case class Generator(isotope: Char) extends Item
case class Microchip(isotope: Char) extends Item

case class Floor(nr: Int, items: List[Item])

case object InputParser extends JavaTokenParsers {

  private val floors = Map[String, Int](
    "first" -> 1,
    "second" -> 2,
    "third" -> 3,
    "fourth" -> 4
  )

  def apply(input: String): List[Floor] = {
    val parsed = parseAll(decode, input)
    if (parsed.successful)
      parsed.get
    else
      List()
  }

  private def decode: Parser[List[Floor]] =
    rep("The " ~ ident ~ "floor contains" ~ items ~ "." ^^ {
      case _ ~ floor ~ _ ~ values ~ _ => Floor(floors(floor), values)
    })

  private def items: Parser[List[Item]] =
    "nothing relevant" ^^ { _ => List() } |
    rep(",".? ~ "and".? ~ item ^^ { case _ ~ _ ~ value => value })

  private def item: Parser[Item] =
    generator | microchip

  private def generator: Parser[Generator] =
    "a" ~ ident ~ "generator" ^^ { case _ ~ name ~ _ => Generator(name.charAt(0).toUpper) }

  private def microchip: Parser[Microchip] =
    "a" ~ ident ~ "-compatible microchip" ^^ { case _ ~ name ~ _ => Microchip(name.charAt(0).toUpper) }
}
