package part1

import scala.util.parsing.combinator.JavaTokenParsers

sealed trait Item {
  val Id: String
  val isGenerator: Boolean
  val isMicrochip: Boolean
}
case class Generator(name: String) extends Item {
  lazy val Id: String = s"${name(0)}G".toUpperCase
  override def toString: String = Id

  val isGenerator: Boolean = true
  val isMicrochip: Boolean = false
}
case class Microchip(generator: String) extends Item {
  lazy val Id: String = s"${generator(0)}M".toUpperCase
  override def toString: String = Id

  val isGenerator: Boolean = false
  val isMicrochip: Boolean = true
}

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
    "a" ~ ident ~ "generator" ^^ { case _ ~ name ~ _ => Generator(name) }

  private def microchip: Parser[Microchip] =
    "a" ~ ident ~ "-compatible microchip" ^^ { case _ ~ name ~ _ => Microchip(name) }
}
