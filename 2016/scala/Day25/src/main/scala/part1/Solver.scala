package part1

import scala.annotation.tailrec

class Solver(input: Stream[String]) {

  private val instructions: Array[Instr] = input.map(InputParser(_)).toArray
  private val infinite = 1000

  def solve: Int = {
    iterate(0, 0)
  }

  @tailrec
  private def iterate(a: Int, transmit: Int, pos: Int = 0, taken: Int = 0): Int = {
    if (taken < infinite) {
      execute(instructions, pos) match {
        case (`transmit`, nextPos) =>
          iterate(a, if (transmit == 0) 1 else 0, nextPos, taken + 1)
        case _ =>
          Registers.reset()
          Registers("a", a + 1)
          iterate(a + 1, 0)
      }
    } else {
      a
    }
  }

  @tailrec
  private def execute(instructions: Array[Instr], pos: Int = 0): (Int, Int) = {
    if (pos < instructions.length)
      instructions(pos) match {
        case Cpy(v, r: Register) =>
          r.value = v.value
          execute(instructions, pos + 1)
        case Cpy(_, _) =>
          execute(instructions, pos + 1)
        case Inc(r) =>
          r.value += 1
          execute(instructions, pos + 1)
        case Dec(r) =>
          r.value -= 1
          execute(instructions, pos + 1)
        case Jnz(x, j) =>
          if (x.value != 0) {
            execute(instructions, pos + j.value)
          } else {
            execute(instructions, pos + 1)
          }
        case Tgl(r) =>
          val index = pos + r.value
          toggleInstruction(index, instructions)
          execute(instructions, pos + 1)
        case Out(x) =>
          (x.value, pos + 1)
      }
    else
      (-1, -1)
  }

  def toggleInstruction(index: Int, instructions: Array[Instr]): Unit = {
    if (index < 0 || index >= instructions.length)
      ()
    else
      instructions(index) = instructions(index) match {
        case Inc(r) => Dec(r)
        case Dec(r) => Inc(r)
        case Tgl(r) => Inc(r)
        case Jnz(x, y) => Cpy(x, y)
        case Cpy(x, y) => Jnz(x, y)
        case Out(x) => Out(x)
      }
  }
}
