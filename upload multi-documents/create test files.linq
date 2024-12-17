<Query Kind="Statements" />

var alphabet = Enumerable.Range(0, 26).Select(n => (char)(n + (int)'A')).ToArray();
var stringItems =
	CountInBase(@base: 26, nb_digits: 2)
		.Select(state => new string(state.Select(n => alphabet[n]).ToArray()));

stringItems.ToList().ForEach(stringItem => {
	File.WriteAllText(
		path: @$"C:\Users\deschaseauxr\Documents\Donum\documents de test d'insertion\{stringItem}.txt",
		contents: stringItem);
});

static IEnumerable<int[]> CountInBase(int @base, int nb_digits) {
	var state = Enumerable.Repeat(element: 0, count: nb_digits).ToArray();
	yield return state;
	var max = Convert.ToInt32(Math.Pow(@base, nb_digits)) - 1;
	for (int i = max - 1; i >= 0; i--) {
		var position = state.Length - 1;
		while (state[position] == @base - 1) {
			state[position] = 0;
			position--;
		}
		state[position]++;
		yield return state;		
	}
}
