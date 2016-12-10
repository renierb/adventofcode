package part1

import scala.util.parsing.combinator.JavaTokenParsers

trait DomainDef {

  object Decoder extends JavaTokenParsers {
    def apply(input: String): Seq[Section] = {
      val parsed = parseAll(decode, input)
      if (parsed.successful)
        parsed.get
      else
        Seq()
    }

    private def decode: Parser[List[Section]] =
      rep(ident ^^ { value => Section(Marker(0, 0), Data(value)) } | marker >> data ^^ { section => section })

    private def marker: Parser[Marker] =
      "(" ~ wholeNumber ~ "x" ~ wholeNumber ~ ")" ^^ { case _ ~ take ~ _ ~ repeat ~ _ => Marker(take.toInt, repeat.toInt) }

    private def data(m: Marker): Parser[Section] =
      s".{${m.take}}".r ^^ { values => Section(m, Data(values.mkString)) }
  }

  case class Section(marker: Marker, data: Data) {
    def length: Int = {
      (marker.take * marker.repeat) + data.value.length - marker.take
    }
  }

  case class Marker(take: Int, repeat: Int) {
    override def toString: String = {
      s"(${take}x$repeat)"
    }
  }

  case class Data(value: String)
}
