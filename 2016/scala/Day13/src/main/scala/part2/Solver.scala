package part2

import scala.collection.mutable

trait Solver extends DomainDef {

  val maxSteps: Int
  val allExplored: mutable.Set[Block] = mutable.Set()

  def neighborsWithHistory(b: Block, history: List[Move]): Stream[(Block, List[Move])] = {
    for( (block, move) <- b.legalNeighbors.toStream ) yield (block, move :: history)
  }

  def newNeighborsOnly(neighbors: Stream[(Block, List[Move])],
                       explored: Set[Block]): Stream[(Block, List[Move])] = {
    for( (block, moveList) <- neighbors if !explored.contains(block) ) yield (block, moveList)
  }

  def from(initial: Stream[(Block, List[Move])], explored: Set[Block]): Stream[(Block, List[Move])] = initial match {
    case Stream.Empty => Stream.empty

    case (block, moveList) #:: tail =>
      if (moveList.length <= maxSteps) {
        allExplored += block
      }
      val newExplored = explored + block
      val newNeighbors = newNeighborsOnly(neighborsWithHistory(block, moveList), newExplored)

      newNeighbors ++ from(tail ++ newNeighbors, newExplored)
  }

  lazy val pathsFromStart: Stream[(Block, List[Move])] =
    from((startSanta, List[Move]()) #:: Stream[(Block, List[Move])](), Set())

  lazy val pathsToGoal: Stream[(Block, List[Move])] = {
    pathsFromStart filter {
      case (b, moves) => moves.length <= maxSteps
    }
  }

  lazy val solution: Int = {
    pathsToGoal match {
      case Stream.Empty => Nil
      case (block, moveList) #:: tail => moveList
    }
    allExplored.size
  }
}