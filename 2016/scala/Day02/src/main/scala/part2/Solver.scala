package part2

class Solver(input: Array[String]) extends DomainDef {

  def solve(instructions: Array[String] = input, code: List[Char] = List()): List[Char] = {
    if (instructions.isEmpty) {
      code.reverse
    } else {
      val i = instructions.head
      val key = if (code.isEmpty) '5' else code.head
      solve(instructions.tail, CodeInterpreter(key).interpret(i) :: code)
    }
  }
}