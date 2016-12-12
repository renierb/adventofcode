package part1

class Solver(input: String) {
  import Solver._

  def solve: Int = {
    def moveItems(state: State, fromFloor: Int = 1, history: Moves = List()): Set[Moves] = {
      if (state.forall(_ == 4))
        Set(state :: history)
      else
        (for {
          toFloor <- floorsFrom(fromFloor)
          if state.exists(_._2 == toFloor)
          move <- combinations(state, fromFloor, toFloor) if move.nonEmpty
          if !history.contains(move)
          moves <- moveItems(move, toFloor, move :: history)
        } yield moves).toSet
    }

    val floors = InputParser(input)
    val startState = floors.flatMap(f => f.items.map(i => (i.Id, f.nr)))

    val solutions = moveItems(startState, 1, List(startState))
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
    else if (floor == 4)
      List(3)
    else
      List(floor + 1, floor - 1)
  }
}

object Solver {
  type State = List[(String, Int)]
  type Moves = List[State]

  def combinations(state: State, fromFloor: Int, toFloor: Int): List[State] = {
    if (state.isEmpty)
      List(List())
    else {
      for {
        (item, floor) <- state if floor == fromFloor
        //other <- combinations(state.diff(Seq((item, floor))), fromFloor, toFloor)
        //diff = state.tail.diff(other)
        //if other.forall(isValid(_, state))  isValid((item, toFloor), other)
      } yield (item, toFloor) :: state.diff(Seq((item, floor)))
      //yield (item, toFloor) :: (other ::: state.tail.diff(other))
    }
  }

  def isValid(item: (String, Int), items: State): Boolean = {
    items.isEmpty ||
    items.forall {
      case (i, f) =>
        if (item._2 == f)
          item._1(0) == i(0) || item._1(1) == i(1)
        else
          true
    }
  }
}