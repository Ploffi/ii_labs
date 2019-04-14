const PlayerTypes = {
  Defends: 'защита'
};


class Club {
    constructor(name, budget) {
        this.id = Math.random().toString(10);
        this.name = name;
        this.budget = budget;
    }

    pay(sum) {
        return this.budget -= sum;
    }
}

class Director {
    get clubId() {
        return this.club.id;
    };

    constructor(name, club) {
        this.club = club;
        this.name = name;
        club.director = this;
    }

    approveContract(player) {
        if (player.price < 500) {
            player.clubId = this.clubId;
            this.club.pay(player.price);
            return true;
        }
        return false;
    }

    fireCoach(coach) {
        coach.club = null;
        this.club.coach = null;
    }

    fireManager(manager) {
        manager.club = null;
        this.club.manager = null;
    }

    hireCoach(coach) {
        coach.club = this.club;
        this.club.coach = coach;
    }

    hireManager(manager) {
        manager.club = this.club;
        this.club.manager = manager;
    }
}

class Coach {
    constructor(name) {
        this.name = name;
    }

    requirePlayer() {
        this.club.manager.startPlayerResearch(PlayerTypes.Defends);
    }

    approvePlayer(player) {
        return player.type === PlayerTypes.Defends;
    }
}

class Manager {
    constructor(name) {
        this.name = name;
    }

    startPlayerResearch(playerType) {
        console.log(`Менеджер ${this.name}: Начат поиск игрока с типом ${playerType}`);
    }

    askPlayerApproveFromCoach(player) {
        return this.club.coach.approvePlayer(player);
    }

    askPlayerApproveFromDirector(player) {
        return this.club.director.approveContract(player);
    }
}

class Player {
    constructor(name, price, type) {
        this.name = name;
        this.price = price;
        this.type = type;
    }

    goToClub(club) {
        this.clubId = club.id;
    }
}

module.exports = {
    PlayerTypes,
    Club,
    Coach,
    Manager,
    Player,
    Director
};
