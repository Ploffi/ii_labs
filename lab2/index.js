const { Club, Coach, Director, Manager, Player, PlayerTypes} = require('./classes');


const club = new Club('Локоматив', 1000);

const director = new Director('Андрей Иванов', club);

const coach = new Coach('Максим Петров');
const manager = new Manager('Алим Зайцев');

director.hireCoach(coach);
director.hireManager(manager);

coach.requirePlayer();

const player = new Player('Дэвид Бекхэм', 200, PlayerTypes.Defends);

const coachApprove = manager.askPlayerApproveFromCoach(player);
if (coachApprove) {
    manager.askPlayerApproveFromDirector(player);
}


