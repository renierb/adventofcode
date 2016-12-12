package part1

class Solver(input: String) {

  type State = List[Int]
  type Moves = List[State]

  def solve: Int = {
    def placeItems(state: State, floor: Int = 1, history: Moves = List()): Set[Moves] = {
      if (state.forall(_ == 4))
        Set(state :: history)
      else
//        for {
//          itemsToTake <- state.indices.combinations(2)
//          floors <- floorsFrom(floor)
//        }
      ???
    }

    val floors = InputParser(input)
    val items = floors.flatMap(_.items.map(_.Id)).sorted.toArray

    val solutions = placeItems(items.map(itemsFloors(floors)(_)).toList)
    solutions.minBy(_.length).length
  }

  def itemsFloors(floors: List[Floor]): Map[String, Int] = {
    floors.flatMap { f =>
      f.items.map { item =>
        item.Id -> f.nr
      }
    }.toMap
  }

  private def floorsFrom(floor: Int): List[Int] = {
    if (floor == 1)
      List(2)
    if (floor == 4)
      List(3)
    else
      List(floor - 1, floor + 1)
  }
}
