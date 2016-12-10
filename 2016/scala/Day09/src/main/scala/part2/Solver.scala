package part2

class Solver(input: String) extends DomainDef {

  def solve: BigInt = {
    val sections = Decoder(input)
    sections.foldLeft(BigInt(0)) { (sum, section) =>
      sum + section.length
    }
  }
}
