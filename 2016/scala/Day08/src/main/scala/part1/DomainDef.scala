package part1

trait DomainDef {

  trait Move {
    def move(key: (Int, Int)): (Int, Int)
  }
  case object Up extends Move {
    override def move(key: (Int, Int)): (Int, Int) = {
      val (row, col) = key
      if (row == 0)
        (row, col)
      else
        (row - 1, col)
    }
  }
  case object Down extends Move {
    override def move(key: (Int, Int)): (Int, Int) = {
      val (row, col) = key
      if (row == 2)
        (row, col)
      else
        (row + 1, col)
    }
  }
  case object Left extends Move {
    override def move(key: (Int, Int)): (Int, Int) = {
      val (row, col) = key
      if (col == 0)
        (row, col)
      else
        (row, col - 1)
    }
  }
  case object Right extends Move {
    override def move(key: (Int, Int)): (Int, Int) = {
      val (row, col) = key
      if (col == 2)
        (row, col)
      else
        (row, col + 1)
    }
  }

  val keys = Map(
    1 -> (0, 0), 2 -> (0, 1), 3 -> (0, 2),
    4 -> (1, 0), 5 -> (1, 1), 6 -> (1, 2),
    7 -> (2, 0), 8 -> (2, 1), 9 -> (2, 2)
  )

  import scala.collection.{IndexedSeq => $}
  val keypad = $(
    $(1, 2, 3),
    $(4, 5, 6),
    $(7, 8, 9)
  )

  case class CodeInterpreter(startKey: Int) {

    def interpret(instructions: String): Int = {
      val (row, col) = instructions.foldLeft(keys(startKey)) { (k, i) =>
        i match {
          case 'U' => Up.move(k)
          case 'D' => Down.move(k)
          case 'L' => Left.move(k)
          case 'R' => Right.move(k)
        }
      }
      keypad(row)(col)
    }
  }
}
