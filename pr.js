const SHIFT = 8;
function program(shift = SHIFT) {
  const numbers = process.argv.slice(2).map(i => parseFloat(i));

  if (numbers.length < shift + 1) {
    console.error("Размер массива меньше 9");
    return;
  }
  if (numbers.includes(NaN)) {
    console.error("Введены невалидные значения");
    return;
  }

  let minimum = Infinity;

  for (let i = 0; i + shift < numbers.length; i++) {
    minimum = Math.min(numbers[i] + numbers[shift + i], minimum);
  }

  console.log(minimum);
}

program();

// const genArray = (gen, length) => {
//   return Array.from(Array(length)).map(gen);
// };

// const check = (expected, array) => {
//   const result = getMinimumOfShiftedArray(array);
//   console.assert(result === expected, `exp: ${expected}, got: ${result}`);
// };

// check(8, genArray((_, i) => i, 9));

// check(8, genArray((_, i) => i, 15));

// check(12, genArray((_, i) => i + 2, 15));

// check(20, genArray((_, i) => 10, 15));

// check(0, genArray((_, i) => 0, 15));
