package part1

import scala.util.parsing.combinator.JavaTokenParsers

object InputParser extends JavaTokenParsers {

  def apply(input: String): Instr = {
    val parsed = parseAll(decode, input)
    if (parsed.successful)
      parsed.get
    else
      throw new Exception("Failed to parse instruction")
  }

  private def decode: Parser[Instr] =
    "^cpy".r ~ value ~ ident ^^ { case _ ~ p1 ~ p2 => Cpy(p1, Register(p2)) } |
    "^inc".r ~ ident ^^ { case _ ~ name => Inc(Register(name)) } |
    "^dec".r ~ ident ^^ { case _ ~ name => Dec(Register(name)) } |
    "^jnz".r ~ value ~ value ^^ { case _ ~ p1 ~ p2 => Jnz(p1, p2) } |
    "^tgl".r ~ ident ^^ { case _ ~ name => Tgl(Register(name)) } |
    "^out".r ~ value ^^ { case _ ~ p1 => Out(p1) }

  private def value: Parser[Value] =
    ident ^^ { name => Register(name) } |
    wholeNumber ^^ { value => Number(value.toInt) }
}
