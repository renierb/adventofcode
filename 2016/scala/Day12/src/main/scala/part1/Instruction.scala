package part1

import scala.collection.mutable

object Registers {
  private val registers = mutable.Map("a" -> 0, "b" -> 0, "c" -> 0, "d" -> 0)

  def apply(name: String): Int = registers(name)
  def apply(name: String, value: Int): Unit = registers(name) = value

  def reset(): Unit = {
    registers("a") = 0
    registers("b") = 0
    registers("c") = 0
    registers("d") = 0
  }
}

sealed trait Value {
  def value: Int
}
case class Number(n: Int) extends Value {
  def value: Int = n
}
case class Register(name: String) extends Value {
  def value: Int = Registers(name)
  def value_=(value: Int): Unit = Registers(name, value)
}

sealed trait Instr
case class Cpy(v: Value, r: Register) extends Instr
case class Inc(r: Register) extends Instr
case class Dec(r: Register) extends Instr
case class Jnz(x: Value, j: Number) extends Instr
