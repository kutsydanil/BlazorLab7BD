//Функция получения данных и занесения в таблицу 
async function GetAllFilms() {
	const response = await fetch("/api/films", {
		method: "GET",
		headers: { "Accept": "application/json" }
	});

	if (response.ok === true) {
		const films = await response.json();
		let rows = document.querySelector("tbody");
		films.forEach(film => {
			rows.append(row(film));
		});
	}
}

//Функция получения всех жанров в элемент html-разметки (select)
async function GetAllGenres() {
	var selectList = filmForm.genre;
	const response = await fetch("/api/genres", {
		method: "GET",
		headers: { "Accept": "application/json" }
	});
	if (response.ok === true) {
		const genres = await response.json();
		genres.forEach(genre => {
			var option = document.createElement("option");
			option.text = genre.name;
			option.value = parseInt(genre.id);
			selectList.append(option)
		});
	}
}

//Функция получения всех стран-производителей в элемент html-разметки (select)
async function GetAllCountryProductions() {
	var selectList = filmForm.countryProduction;
	const response = await fetch("/api/countryProductions", {
		method: "GET",
		headers: { "Accept": "application/json" }
	});
	if (response.ok === true) {
		const countryProductions = await response.json();
		countryProductions.forEach(countryProduction => {
			var option = document.createElement("option");
			option.text = countryProduction.name;
			option.value = parseInt(countryProduction.id);
			selectList.append(option);
		});
	}
}

//Функция получения всех компаний-производителей в элемент html-разметки (select)
async function GetAllFilmProduction() {
	var selectList = filmForm.filmProduction;
	const response = await fetch("/api/filmProductions", {
		method: "GET",
		headers: { "Accept": "application/json" }
	});
	if (response.ok === true) {
		const filmsProductions = await response.json();
		filmsProductions.forEach(filmsProduction => {
			var option = document.createElement("option");
			option.text = filmsProduction.name;
			option.value = parseInt(filmsProduction.id);
			selectList.append(option);
		});
	}
}