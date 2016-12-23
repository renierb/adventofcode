package part1

import scala.annotation.tailrec

class Solver(input: Stream[String]) {

  Registers("a", 7)

  def solve: Int = {
    val instructions: Array[Instr] = input.map(InputParser(_)).toArray
    execute(instructions)

    Registers("a")
  }

  @tailrec
  private def execute(instructions: Array[Instr], pos: Int = 0): Unit = {
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
      }
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
      }
  }
}
