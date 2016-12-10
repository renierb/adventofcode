package part1

class Solver(input: String) extends DomainDef {

  def solve: Int = {
    val sections = Decoder(input)
    sections.foldLeft(0) { (sum, section) =>
      sum + section.length
    }
  }
}
