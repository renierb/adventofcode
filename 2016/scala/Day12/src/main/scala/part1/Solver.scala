package part1

import scala.annotation.tailrec

class Solver(input: Stream[String]) {

  def solve: Int = {
    val instructions: Array[Instr] = input.map(InputParser(_)).toArray
    execute(instructions)

    Registers("a")
  }

  @tailrec
  private def execute(instructions: Array[Instr], pos: Int = 0): Unit = {
    if (pos < instructions.length)
      instructions(pos) match {
        case Cpy(v, r) =>
          r.value = v.value
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
      }
  }
}
