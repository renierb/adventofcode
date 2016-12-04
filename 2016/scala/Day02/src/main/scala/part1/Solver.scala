package part1

class Solver(input: Array[String]) extends DomainDef {

  def solve(instructions: Array[String] = input, code: List[Int] = List()): List[Int] = {
    if (instructions.isEmpty) {
      code.reverse
    } else {
      val i = instructions.head
      val key = if (code.isEmpty) 5 else code.head
      solve(instructions.tail, CodeInterpreter(key).interpret(i) :: code)
    }
  }
}