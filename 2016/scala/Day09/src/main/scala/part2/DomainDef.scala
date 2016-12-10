package part2

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
    def length: BigInt = {
      def iter(sections: Seq[Section], acc: BigInt = 0): BigInt = {
        if (sections.isEmpty)
          acc
        else
          iter(sections.tail, acc + sections.head.length)
      }

      if (!data.isCompressed)
        (marker.take * marker.repeat) + data.length - marker.take
      else
        marker.repeat * iter(Decoder(data.value))
    }
  }

  case class Marker(take: Int, repeat: Int) {
    override def toString: String = {
      s"(${take}x$repeat)"
    }
  }

  case class Data(value: String) {
    def length: Int = {
      value.length
    }

    def isCompressed: Boolean = {
      value.contains("(")
    }
  }
}