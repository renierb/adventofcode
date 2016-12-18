package part1

class Solver(input: String, rows: Int) {

  def solve: Int = {
    val result = (0 until rows - 1).foldLeft((input, countSafeTiles(input))) {
      case ((row, c), _) =>
        val next = nextRow(row)
        (next, c + countSafeTiles(next))
    }

    val (_, count) = result
    count
  }

  def countSafeTiles(row: String): Int = {
    row.count(_ == '.')
  }

  def nextRow(row: String): String = {
    def isTrap(index: Int): Boolean = {
      val l = if (index > 0) row(index - 1) else '.'
      val c = if (index < row.length) row(index) else '.'
      val r = if (index < row.length - 1) row(index + 1) else '.'

      if(c == '^')
        (l == '^' && r != '^') || (l != '^' && r == '^')
      else
        (l == '^' && r != '^') || (l != '^' && r == '^')
    }

    if (row.isEmpty)
      row
    else {
      val nextRow = new StringBuilder(row.length)
      (0 until row.length).foldLeft(nextRow) { (b, i) =>
        b.append(if (isTrap(i)) '^' else '.')
      }
      nextRow.toString
    }
  }
}
