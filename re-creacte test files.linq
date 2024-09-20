<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

static Random dice = new Random();
const string directory = @"C:\Users\deschaseauxr\Documents\Donum\documents de test d'insertion";
async Task Main() {
	Directory.Delete(path: directory, true);
	Directory.CreateDirectory(path: directory);
	await Task.WhenAll(
		Enumerable
			.Range(0, 3)
			.Select(async _ => {
				var file_name = $"{get_random_word(10)}.txt";
				var file_path = Path.Combine(directory, file_name);
				var content = get_random_word(10);
				await File.WriteAllTextAsync(
					path: file_path,
					contents: content);
			}));
}

static string get_random_word(int length) =>
	new string(new int[length].Select(_ => (char)(dice.Next(26) + (int)'A')).ToArray());
